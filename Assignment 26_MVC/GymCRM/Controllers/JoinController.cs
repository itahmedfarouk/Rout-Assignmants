using System;
using System.Linq;
using System.Text.Json;
using GymCRM.Data;
using GymCRM.Model;
using GymCRM.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;            
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks; 

namespace GymCRM.Controllers
{
    [Route("join")]
    public class JoinController : Controller
    {
        private readonly GymCRMContext _db;
        private readonly Services.IPricingService _pricing;
        private readonly Services.IPaymentService _payment;
        private readonly Services.ILocationService _location;
        private readonly UserManager<ApplicationUser> _users;  
        private const string SessionKey = "GymCRM.JoinFlow";

        public JoinController(
            GymCRMContext db,
            Services.IPricingService pricing,
            Services.IPaymentService payment,
            Services.ILocationService location,
            UserManager<ApplicationUser> users)                 
        {
            _db = db; _pricing = pricing; _payment = payment; _location = location; _users = users;
        }

        [HttpGet("")]
        [HttpGet("step1")]
        public IActionResult Step1()
        {
            var vm = new Step1SelectViewModel
            {
                Countries = GetCountries(),
                Plans = GetPlans()
            };
            return View(vm);
        }

        [HttpGet("cities")]
        public IActionResult Cities(int countryId)
        {
            var data = _db.Cities.Where(c => c.CountryId == countryId)
                        .Select(c => new { id = c.Id, name = c.NameAr })
                        .OrderBy(x => x.name)
                        .ToList();
            return Json(data);
        }

        [HttpGet("branches")]
        public IActionResult Branches(int cityId, Gender? gender)
        {
            var q = _db.Branches.Where(b => b.CityId == cityId);
            if (gender.HasValue) q = q.Where(b => b.AllowedGender == gender.Value);

            var data = q.Select(b => new { id = b.Id, name = b.NameAr }).ToList();
            return Json(data);
        }

        [HttpGet("price")]
        public IActionResult Price(int planId, string? coupon)
        {
            var total = _pricing.Estimate(planId, coupon);
            var subtotal = Math.Round(total / 1.15m, 2);
            var vat = Math.Round(total - subtotal, 2);
            return Json(new { subtotal, vat, total });
        }

        [HttpGet("nearest")]
        public IActionResult Nearest(double lat, double lng, Gender? gender, int? cityId, int take = 3)
        {
            var result = _location.GetNearestBranches(lat, lng, gender, cityId, take);
            return Json(result);
        }

        [HttpPost("step1")]
        [ValidateAntiForgeryToken]
        public IActionResult Step1(Step1SelectViewModel m)
        {
            if (m.Gender is null) ModelState.AddModelError(nameof(m.Gender), "النوع مطلوب.");
            if (m.CountryId is null) ModelState.AddModelError(nameof(m.CountryId), "الدولة مطلوبة.");
            if (m.CityId is null) ModelState.AddModelError(nameof(m.CityId), "المدينة مطلوبة.");
            if (m.BranchId is null) ModelState.AddModelError(nameof(m.BranchId), "الفرع مطلوب.");
            if (m.PlanId is null) ModelState.AddModelError(nameof(m.PlanId), "الخطة مطلوبة.");

            if (m.CityId is not null && m.BranchId is not null)
            {
                var ok = _db.Branches.Any(b =>
                    b.Id == m.BranchId &&
                    b.CityId == m.CityId &&
                    (!m.Gender.HasValue || b.AllowedGender == m.Gender.Value));
                if (!ok) ModelState.AddModelError(nameof(m.BranchId), "الفرع غير متوافق مع المدينة/النوع.");
            }

            if (!ModelState.IsValid)
            {
                m.Countries = GetCountries();
                m.Plans = GetPlans();
                m.Cities = Enumerable.Empty<SelectListItem>();
                m.Branches = Enumerable.Empty<SelectListItem>();
                return View(m);
            }

            m.EstimatedTotal = _pricing.Estimate(m.PlanId, m.CouponCode);
            Save(".step1", m);
            return RedirectToAction(nameof(Step2));
        }

        [HttpGet("step2")]
        public IActionResult Step2() => View(new Step2PersonalViewModel());

        [HttpPost("step2")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Step2(Step2PersonalViewModel m)
        {
            if (!ModelState.IsValid) return View(m);

            var userByName = await _users.FindByNameAsync(m.Username);
            if (userByName != null)
                ModelState.AddModelError(nameof(m.Username), "اسم المستخدم مستخدم من قبل.");

            var userByEmail = await _users.FindByEmailAsync(m.Email);
            if (userByEmail != null)
                ModelState.AddModelError(nameof(m.Email), "البريد مستخدم من قبل.");

            if (!ModelState.IsValid) return View(m);

            Save(".step2", m);
            return RedirectToAction(nameof(Step3));
        }


        [HttpGet("step3")]
        public IActionResult Step3()
        {
            var s1 = Load<Step1SelectViewModel>(".step1");
            var s2 = Load<Step2PersonalViewModel>(".step2");
            if (s1 is null || s2 is null) return RedirectToAction(nameof(Step1));

            var total = _pricing.Estimate(s1.PlanId, s1.CouponCode);
            var subtotal = Math.Round(total / 1.15m, 2);
            var vat = Math.Round(total - subtotal, 2);

            var cityName = s1.CityId.HasValue
                ? _db.Cities.Where(c => c.Id == s1.CityId).Select(c => c.NameAr).FirstOrDefault()
                : null;
            var branchName = s1.BranchId.HasValue
                ? _db.Branches.Where(b => b.Id == s1.BranchId).Select(b => b.NameAr).FirstOrDefault()
                : null;
            var planTitle = s1.PlanId.HasValue
                ? _db.MembershipPlans.Where(p => p.Id == s1.PlanId).Select(p => p.TitleAr).FirstOrDefault()
                : null;

            var vm = new Step3ReviewViewModel
            {
                Step1 = s1,
                Step2 = s2,
                Subtotal = subtotal,
                Vat = vat,
                Total = total,
                CityName = cityName,
                BranchName = branchName,
                PlanTitle = planTitle
            };
            return View(vm);
        }

        [HttpPost("confirm")]
        [ValidateAntiForgeryToken]
        public IActionResult Confirm()
        {
            var s1 = Load<Step1SelectViewModel>(".step1");
            var s2 = Load<Step2PersonalViewModel>(".step2");
            if (s1 is null || s2 is null) return RedirectToAction(nameof(Step1));

            var plan = _db.MembershipPlans.Find(s1.PlanId);
            var branch = _db.Branches.Find(s1.BranchId);
            if (plan == null || branch == null) return RedirectToAction(nameof(Step1));

            var customer = new Customer
            {
                FullName = s2.FullName,
                NationalId = s2.NationalId,
                Phone = s2.Phone,
                Email = s2.Email,
                Gender = s1.Gender ?? Gender.Male
            };
            _db.Customers.Add(customer);
            _db.SaveChanges();

            var total = _pricing.Estimate(s1.PlanId, s1.CouponCode);
            var subtotal = Math.Round(total / 1.15m, 2);
            var vat = Math.Round(total - subtotal, 2);

            var sub = new Subscription
            {
                CustomerId = customer.Id,
                BranchId = branch.Id,
                MembershipPlanId = plan.Id,
                AppliedCoupon = s1.CouponCode,
                Subtotal = subtotal,
                VatAmount = vat,
                Total = total,
                Status = "Pending"
            };
            _db.Subscriptions.Add(sub);
            _db.SaveChanges();

            HttpContext.Session.SetInt32("GymCRM.CurrentCustomerId", customer.Id);
            HttpContext.Session.SetInt32("GymCRM.LastSubscriptionId", sub.Id);

            return RedirectToAction(nameof(Step4), new { id = sub.Id });
        }

        [HttpGet("step4")]
        public IActionResult Step4(int id)
        {
            var sub = _db.Subscriptions.Find(id);
            if (sub == null) return RedirectToAction(nameof(Step1));
            return View(new Step4PaymentViewModel { SubscriptionId = id, Total = sub.Total });
        }

        [HttpPost("pay")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pay(Step4PaymentViewModel m)
        {
            var sub = _db.Subscriptions.Find(m.SubscriptionId);
            if (sub == null) return RedirectToAction(nameof(Step1));

            var res = _payment.Charge(m.SubscriptionId);
            if (res.Success)
            {
                var plan = _db.MembershipPlans.Find(sub.MembershipPlanId);
                var start = sub.StartDate ?? DateTime.UtcNow.Date;
                var months = plan?.DurationMonths > 0 ? plan.DurationMonths : 1;

                sub.Status = "Paid";
                sub.PaymentRef = res.Ref;
                sub.PaidAt = DateTime.UtcNow;
                sub.StartDate = start;
                sub.EndDate = start.AddMonths(months);
                _db.SaveChanges();
                var s2 = Load<Step2PersonalViewModel>(".step2");
                var customer = _db.Customers.Find(sub.CustomerId);

                if (s2 != null && customer != null)
                {
                    var user = await _users.FindByNameAsync(s2.Username) ?? await _users.FindByEmailAsync(s2.Email);
                    if (user == null)
                    {
                        user = new ApplicationUser
                        {
                            UserName = s2.Username,
                            Email = s2.Email,
                            PhoneNumber = s2.Phone,
                            DisplayName = s2.FullName
                        };
                        var cr = await _users.CreateAsync(user, s2.Password);
                        if (!cr.Succeeded)
                        {
                            TempData["GymCRM.IdentityErr"] = string.Join(" | ", cr.Errors.Select(e => e.Description));
                        }
                    }
                    if (user != null)
                    {
                        customer.UserId = user.Id;
                        _db.SaveChanges();
                    }
                }

                HttpContext.Session.SetInt32("GymCRM.CurrentCustomerId", sub.CustomerId);
                HttpContext.Session.SetInt32("GymCRM.LastSubscriptionId", sub.Id);
                TempData["GymCRM.JustPaid"] = "1";

                return RedirectToAction("Dashboard", "Account",
                    new { id = sub.CustomerId, subscriptionId = sub.Id });
            }

            TempData["GymCRM.PaymentRef"] = res.Ref;
            return RedirectToAction(nameof(Result), new { id = m.SubscriptionId, ok = res.Success });
        }

        [HttpGet("result")]
        public IActionResult Result(int id, bool ok) => View(model: ok);

        private IEnumerable<SelectListItem> GetCountries() =>
            _db.Countries
               .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.NameAr })
               .ToList();

        private IEnumerable<SelectListItem> GetPlans() =>
            _db.MembershipPlans.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = $"{p.TitleAr} — {p.PriceMonthly} ر.س/ش"
            }).ToList();

        private void Save<T>(string tail, T model) =>
            HttpContext.Session.SetString(SessionKey + tail, JsonSerializer.Serialize(model));

        private T? Load<T>(string tail)
        {
            var s = HttpContext.Session.GetString(SessionKey + tail);
            return string.IsNullOrEmpty(s) ? default : JsonSerializer.Deserialize<T>(s);
        }
    }
}
