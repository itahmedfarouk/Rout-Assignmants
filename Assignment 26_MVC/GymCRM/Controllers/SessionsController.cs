using System;
using System.Linq;
using GymCRM.Data;
using GymCRM.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymCRM.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SessionsController : Controller
    {
        private readonly GymCRMContext _db;
        public SessionsController(GymCRMContext db) { _db = db; }

        public IActionResult Index()
        {
            var q = from s in _db.ScheduledSessions
                    orderby s.StartAtUtc descending
                    select new
                    {
                        s.Id,
                        Type = s.SessionType.TitleAr,
                        Branch = s.Branch.NameAr,
                        s.StartAtUtc,
                        s.DurationMin,
                        s.Capacity,
                        s.IsCanceled
                    };
            return View(q.ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Types = _db.SessionTypes.Where(t => t.IsActive)
                .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.TitleAr }).ToList();
            ViewBag.Branches = _db.Branches
                .Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.NameAr }).ToList();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(int sessionTypeId, int branchId, DateTime startAtLocal, int durationMin, int capacity)
        {
            var startUtc = DateTime.SpecifyKind(startAtLocal, DateTimeKind.Local).ToUniversalTime();
            var s = new ScheduledSession
            {
                SessionTypeId = sessionTypeId,
                BranchId = branchId,
                StartAtUtc = startUtc,
                DurationMin = durationMin,
                Capacity = capacity
            };
            _db.ScheduledSessions.Add(s);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Cancel(int id)
        {
            var s = _db.ScheduledSessions.Find(id);
            if (s == null) return NotFound();
            s.IsCanceled = true;
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
