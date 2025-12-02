using System;
using System.Linq;
using GymCRM.Data;
using GymCRM.Model;
using GymCRM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymCRM.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly GymCRMContext _db;
        private const string SessionCustomerId = "GymCRM.CurrentCustomerId";
        private const string SessionLastSubId = "GymCRM.LastSubscriptionId";

        public AccountController(GymCRMContext db) => _db = db;

        [HttpGet("dashboard")]
        public IActionResult Dashboard(int? id, int? subscriptionId)
        {
            var customerId = id ?? HttpContext.Session.GetInt32(SessionCustomerId);
            if (customerId is null) return RedirectToAction("Step1", "Join");

            var customer = _db.Customers.Find(customerId.Value);
            if (customer is null) return RedirectToAction("Step1", "Join");

            var sub = subscriptionId.HasValue
                ? _db.Subscriptions.FirstOrDefault(s => s.Id == subscriptionId.Value && s.CustomerId == customer.Id)
                : _db.Subscriptions.Where(s => s.CustomerId == customer.Id)
                                   .OrderByDescending(s => s.Id)
                                   .FirstOrDefault();

            string? planTitle = sub != null
                ? _db.MembershipPlans.Where(p => p.Id == sub.MembershipPlanId).Select(p => p.TitleAr).FirstOrDefault()
                : null;

            string? branchName = sub != null
                ? _db.Branches.Where(b => b.Id == sub.BranchId).Select(b => b.NameAr).FirstOrDefault()
                : null;

            string? cityName = sub != null
                ? (from b in _db.Branches
                   join c in _db.Cities on b.CityId equals c.Id
                   where b.Id == sub.BranchId
                   select c.NameAr).FirstOrDefault()
                : null;

            int? daysRem = null;
            bool canStartNew = false;
            if (sub?.Status == "Paid" && sub.EndDate.HasValue)
            {
                var today = DateTime.UtcNow.Date;
                daysRem = (int)Math.Ceiling((sub.EndDate.Value.Date - today).TotalDays);
                canStartNew = daysRem <= 5 && daysRem >= 0;
            }

            var utcNow = DateTime.UtcNow;

            var bookings = _db.SessionBookings
                .Include(b => b.ScheduledSession)
                    .ThenInclude(s => s.SessionType)
                .Include(b => b.ScheduledSession)
                    .ThenInclude(s => s.Branch)
                .Where(b => b.CustomerId == customer.Id)
                .OrderByDescending(b => b.ScheduledSession.StartAtUtc)
                .Take(10)
                .AsEnumerable()
                .Select(b =>
                {
                    var startUtc = b.ScheduledSession.StartAtUtc;
                    var endUtc = startUtc.AddMinutes(b.ScheduledSession.DurationMin);

                    var windowOpen = startUtc.AddMinutes(-30);
                    var windowClose = endUtc;

                    var canCancel = b.Status == BookingStatus.Booked &&
                                    startUtc.Subtract(utcNow).TotalHours >= 4;

                    var canCheckIn = b.Status == BookingStatus.Booked &&
                                     b.Attended == null &&
                                     utcNow >= windowOpen && utcNow <= windowClose;

                    return new BookingItemViewModel
                    {
                        BookingId = b.Id,
                        SessionTitle = b.ScheduledSession.SessionType.TitleAr,
                        BranchName = b.ScheduledSession.Branch.NameAr,
                        StartAtLocal = startUtc.ToLocalTime(),
                        DurationMin = b.ScheduledSession.DurationMin,
                        Status = b.Status,
                        CanCancel = canCancel,
                        CanCheckIn = canCheckIn,
                        Attended = b.Attended
                    };
                })
                .ToList();

            var vm = new UserDashboardViewModel
            {
                CustomerId = customer.Id,
                FullName = customer.FullName,
                Email = customer.Email,
                Phone = customer.Phone,
                SubscriptionId = sub?.Id,
                Status = sub?.Status,
                PaymentRef = sub?.PaymentRef,
                AppliedCoupon = sub?.AppliedCoupon,
                Subtotal = sub?.Subtotal,
                Vat = sub?.VatAmount,
                Total = sub?.Total,
                PlanTitle = planTitle,
                BranchName = branchName,
                CityName = cityName,
                StartDate = sub?.StartDate,
                EndDate = sub?.EndDate,
                DaysRemaining = daysRem,
                CanStartNew = canStartNew,
                JustPaid = TempData["GymCRM.JustPaid"] as string == "1",
                Bookings = bookings
            };

            return View("~/Views/Account/Dashboard.cshtml", vm);
        }

        [HttpPost("booking/cancel-request/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult CancelRequest(int id)
        {
            var b = _db.SessionBookings
                       .Include(x => x.ScheduledSession)
                       .FirstOrDefault(x => x.Id == id);

            if (b == null) return NotFound();

            var curCustomerId = HttpContext.Session.GetInt32(SessionCustomerId);
            if (curCustomerId == null || b.CustomerId != curCustomerId.Value)
                return Forbid();

            var diff = b.ScheduledSession.StartAtUtc - DateTime.UtcNow;
            if (diff.TotalHours < 4 || b.Status != BookingStatus.Booked)
            {
                TempData["Err"] = "لا يمكن الإلغاء خلال أقل من 4 ساعات من بداية الجلسة.";
                return RedirectToAction(nameof(Dashboard), new { id = curCustomerId.Value });
            }

            b.Status = BookingStatus.CancelRequested;
            b.CancelRequestedAtUtc = DateTime.UtcNow;
            _db.SaveChanges();

            TempData["Ok"] = "تم إرسال طلب الإلغاء إلى الإدارة.";
            return RedirectToAction(nameof(Dashboard), new { id = curCustomerId.Value });
        }

        // ✅ تأكيد الحضور
        [HttpPost("booking/checkin/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult CheckIn(int id)
        {
            var b = _db.SessionBookings
                       .Include(x => x.ScheduledSession)
                       .FirstOrDefault(x => x.Id == id);

            if (b == null) return NotFound();

            var curCustomerId = HttpContext.Session.GetInt32(SessionCustomerId);
            if (curCustomerId == null || b.CustomerId != curCustomerId.Value)
                return Forbid();

            var nowUtc = DateTime.UtcNow;
            var startUtc = b.ScheduledSession.StartAtUtc;
            var endUtc = startUtc.AddMinutes(b.ScheduledSession.DurationMin);
            var windowOpen = startUtc.AddMinutes(-30);
            var windowClose = endUtc;

            if (b.Status != BookingStatus.Booked || b.Attended != null || nowUtc < windowOpen || nowUtc > windowClose)
            {
                TempData["Err"] = "لا يمكن تأكيد الحضور الآن.";
                return RedirectToAction(nameof(Dashboard), new { id = curCustomerId.Value });
            }

            b.Attended = true;
            b.AttendedAtUtc = nowUtc;
            _db.SaveChanges();

            TempData["Ok"] = "تم تسجيل حضورك بنجاح.";
            return RedirectToAction(nameof(Dashboard), new { id = curCustomerId.Value });
        }
    }
}
