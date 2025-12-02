using GymCRM.Data;
using GymCRM.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace GymCRM.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("admin/sessions")]
    public class AdminSessionsController : Controller
    {
        private readonly GymCRMContext _db;
        public AdminSessionsController(GymCRMContext db) { _db = db; }

        // GET /admin/sessions

        [HttpGet("")]
        public IActionResult Index()
        {
            var list = _db.ScheduledSessions
                .OrderByDescending(s => s.StartAtUtc)
                .Select(s => new AdminSessionRowViewModel
                {
                    Id = s.Id,
                    Type = s.SessionType.TitleAr,
                    Branch = s.Branch.NameAr,
                    StartAtUtc = s.StartAtUtc,
                    DurationMin = s.DurationMin,
                    Capacity = s.Capacity,
                    IsCanceled = s.IsCanceled
                })
                .ToList();

            return View("~/Views/Admin/Sessions/Index.cshtml", list);
        }

        // GET /admin/sessions/create
        [HttpGet("create")]
        public IActionResult Create()
        {
            ViewBag.Types = _db.SessionTypes.Where(t => t.IsActive)
                .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.TitleAr }).ToList();

            ViewBag.Branches = _db.Branches
                .Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.NameAr }).ToList();

            return View("~/Views/Admin/Sessions/Create.cshtml");
        }

        // POST /admin/sessions/create
        [HttpPost("create"), ValidateAntiForgeryToken]
        public IActionResult Create(int sessionTypeId, int branchId, DateTime startAtLocal, int durationMin, int capacity)
        {
            var startUtc = DateTime.SpecifyKind(startAtLocal, DateTimeKind.Local).ToUniversalTime();
            var s = new Model.ScheduledSession
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

        // POST /admin/sessions/cancel/{id}
        [HttpPost("cancel/{id}"), ValidateAntiForgeryToken]
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
