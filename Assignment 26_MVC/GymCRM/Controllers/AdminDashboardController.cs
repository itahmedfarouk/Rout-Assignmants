// Controllers/AdminDashboardController.cs
using System;
using System.Linq;
using GymCRM.Data;
using GymCRM.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymCRM.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminDashboardController : Controller
    {
        private readonly GymCRMContext _db;
        public AdminDashboardController(GymCRMContext db) { _db = db; }

        // GET /admin/dashboard
        [HttpGet("/admin/dashboard")]
        public IActionResult Index(int? countryId, int? cityId, int? branchId)
        {
            var vm = new AdminDashboardViewModel
            {
                SelectedCountryId = countryId,
                SelectedCityId = cityId,
                SelectedBranchId = branchId,
                Countries = _db.Countries
                    .OrderBy(c => c.NameAr)
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.NameAr })
                    .ToList()
            };

            var subsQ = _db.Subscriptions.AsQueryable();

            if (branchId.HasValue)
            {
                subsQ = subsQ.Where(s => s.BranchId == branchId.Value);
            }
            else if (cityId.HasValue)
            {
                subsQ =
                    from s in subsQ
                    join b in _db.Branches on s.BranchId equals b.Id
                    where b.CityId == cityId.Value
                    select s;
            }
            else if (countryId.HasValue)
            {
                subsQ =
                    from s in subsQ
                    join b in _db.Branches on s.BranchId equals b.Id
                    join c in _db.Cities on b.CityId equals c.Id
                    where c.CountryId == countryId.Value
                    select s;
            }

            var now = DateTime.UtcNow;
            vm.TotalSubs = subsQ.Count();
            vm.ActiveSubs = subsQ.Count(s => s.Status == "Paid" && s.StartDate <= now && s.EndDate >= now);
            vm.Revenue = subsQ.Where(s => s.Status == "Paid")
                                 .Select(s => (decimal?)s.Total).Sum() ?? 0m;

            vm.PerPlan =
                (from s in subsQ
                 join p in _db.MembershipPlans on s.MembershipPlanId equals p.Id
                 group new { s, p } by p.TitleAr into g
                 select new AdminDashboardViewModel.PerPlanRow
                 {
                     Plan = g.Key,
                     Count = g.Count(),
                     Revenue = g.Where(x => x.s.Status == "Paid")
                                .Sum(x => (decimal?)x.s.Total) ?? 0m
                 })
                .OrderByDescending(x => x.Count)
                .ToList();

            vm.PerCity =
                (from s in subsQ
                 join b in _db.Branches on s.BranchId equals b.Id
                 join c in _db.Cities on b.CityId equals c.Id
                 group s by c.NameAr into g
                 select new AdminDashboardViewModel.PerCityRow
                 {
                     City = g.Key,
                     Count = g.Count()
                 })
                .OrderByDescending(x => x.Count)
                .ToList();

            if (branchId.HasValue)
            {
                vm.BranchSubscriptions =
                    (from s in _db.Subscriptions
                     join cust in _db.Customers on s.CustomerId equals cust.Id
                     join p in _db.MembershipPlans on s.MembershipPlanId equals p.Id
                     where s.BranchId == branchId.Value
                     orderby s.Id descending
                     select new AdminDashboardViewModel.BranchSubRow
                     {
                         SubscriptionId = s.Id,
                         Customer = cust.FullName,
                         Plan = p.TitleAr,
                         StartDate = s.StartDate,
                         EndDate = s.EndDate,
                         Status = s.Status
                     })
                    .Take(200)
                    .ToList();
            }

            return View("~/Views/Admin/Dashboard/Index.cshtml", vm);
        }

        [HttpGet("/admin/filter/cities")]
        public IActionResult Cities(int countryId)
        {
            var js = _db.Cities
                .Where(c => c.CountryId == countryId)
                .OrderBy(c => c.NameAr)
                .Select(c => new { id = c.Id, name = c.NameAr })
                .ToList();
            return Json(js);
        }

        [HttpGet("/admin/filter/branches")]
        public IActionResult Branches(int cityId)
        {
            var js = _db.Branches
                .Where(b => b.CityId == cityId)
                .OrderBy(b => b.NameAr)
                .Select(b => new { id = b.Id, name = b.NameAr })
                .ToList();
            return Json(js);
        }
    }
}
