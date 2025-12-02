using System;
using System.Linq;
using System.Threading.Tasks;
using GymCRM.Data;
using GymCRM.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymCRM.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BookingsController : Controller
    {
        private readonly GymCRMContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookingsController(GymCRMContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Requests()
        {
            var reqs = _db.SessionBookings
                .Where(b => b.Status == BookingStatus.CancelRequested)
                .Select(b => new {
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

            return View(reqs);
        }

        [HttpPost, ValidateAntiForgeryToken]
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

        [HttpPost, ValidateAntiForgeryToken]
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


        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(int scheduledSessionId, int customerId)
        {
 
            var b = new SessionBooking
            {
                ScheduledSessionId = scheduledSessionId,
                CustomerId = customerId,
                Status = BookingStatus.Booked
            };

            _db.SessionBookings.Add(b);
            _db.SaveChanges();

            return RedirectToAction(nameof(Requests));
        }
    }
}
