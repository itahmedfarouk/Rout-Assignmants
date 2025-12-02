using System;
using System.Linq;
using System.Threading.Tasks;
using GymCRM.Data;
using GymCRM.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GymCRM.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("admin/bookings")]
    public class AdminBookingsController : Controller
    {
        private readonly GymCRMContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminBookingsController(GymCRMContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpGet("requests")]
        public IActionResult Requests()
        {
            var reqs = _db.SessionBookings
                .Where(b => b.Status == BookingStatus.CancelRequested)
                .Select(b => new
                {
                    b.Id,
                    Customer = b.Customer.FullName,
                    Session = b.ScheduledSession.SessionType.TitleAr,
                    Branch = b.ScheduledSession.Branch.NameAr,
                    b.ScheduledSession.StartAtUtc,
                    b.ScheduledSession.DurationMin,
                    b.CancelRequestedAtUtc
                })
                .OrderBy(b => b.StartAtUtc)
                .ToList();

            return View("~/Views/Admin/Bookings/Requests.cshtml", reqs);
        }

        [HttpPost("approve/{id}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {
            var b = _db.SessionBookings.Find(id);
            if (b == null) return NotFound();

            b.Status = BookingStatus.CancelApproved;
            b.AdminDecisionAtUtc = DateTime.UtcNow;
            var user = await _userManager.GetUserAsync(User);
            b.AdminDecisionByUserId = user?.Id;

            _db.SaveChanges();
            return RedirectToAction(nameof(Requests));
        }

        [HttpPost("reject/{id}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id)
        {
            var b = _db.SessionBookings.Find(id);
            if (b == null) return NotFound();

            b.Status = BookingStatus.CancelRejected;
            b.AdminDecisionAtUtc = DateTime.UtcNow;
            var user = await _userManager.GetUserAsync(User);
            b.AdminDecisionByUserId = user?.Id;

            _db.SaveChanges();
            return RedirectToAction(nameof(Requests));
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            var nowUtc = DateTime.UtcNow;


            var raw = _db.ScheduledSessions
                .Include(s => s.SessionType)
                .Include(s => s.Branch)
                .Where(s => !s.IsCanceled && s.StartAtUtc >= nowUtc.AddHours(-1))
                .OrderBy(s => s.StartAtUtc)
                .Take(200)
                .ToList();

            ViewBag.SessionsOptions = raw.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = $"{s.SessionType.TitleAr} — {s.Branch.NameAr} — {s.StartAtUtc.ToLocalTime():yyyy/MM/dd HH:mm}"
            }).ToList();

            ViewBag.SessionBranchMap = raw.ToDictionary(s => s.Id.ToString(), s => s.BranchId);

            return View("~/Views/Admin/Bookings/Create.cshtml");
        }


        [HttpGet("cities")]
        public IActionResult Cities(int countryId)
        {
            var items = _db.Cities
                .Where(c => c.CountryId == countryId)
                .OrderBy(c => c.NameAr)
                .Select(c => new { id = c.Id, name = c.NameAr })
                .ToList();

            return Json(items);
        }

        [HttpGet("branches")]
        public IActionResult Branches(int cityId)
        {
            var items = _db.Branches
                .Where(b => b.CityId == cityId)
                .OrderBy(b => b.NameAr)
                .Select(b => new { id = b.Id, name = b.NameAr })
                .ToList();

            return Json(items);
        }

        [HttpGet("customers")]
        public IActionResult Customers(int branchId)
        {
            var today = DateTime.UtcNow.Date;

            var customers = _db.Subscriptions
                .Include(s => s.Customer)
                .Where(s => s.BranchId == branchId
                            && s.Status == "Paid"
                            && s.StartDate <= today && s.EndDate >= today)
                .Select(s => s.Customer)
                .Distinct()
                .OrderBy(c => c.FullName)
                .Select(c => new { id = c.Id, name = $"{c.FullName} — {c.Phone}" })
                .ToList();

            return Json(customers);
        }

        [HttpPost("create"), ValidateAntiForgeryToken]
        public IActionResult Create(int scheduledSessionId, int customerId)
        {
            var booking = new SessionBooking
            {
                ScheduledSessionId = scheduledSessionId,
                CustomerId = customerId,
                Status = BookingStatus.Booked
            };

            _db.SessionBookings.Add(booking);
            _db.SaveChanges();

            return Redirect("/admin/dashboard");
        }
    }
}
