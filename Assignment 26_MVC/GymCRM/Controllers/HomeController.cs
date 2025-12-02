using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymCRM.Models;
using GymCRM.Data;
using GymCRM.ViewModels;

namespace GymCRM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GymCRMContext _db;

        public HomeController(ILogger<HomeController> logger, GymCRMContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            var vm = new LandingViewModel
            {
                Countries = _db.Countries
                               .OrderBy(c => c.NameAr)
                               .Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                               {
                                   Value = c.Id.ToString(),
                                   Text = c.NameAr
                               })
                               .ToList(),

                Plans = _db.MembershipPlans
                           .OrderBy(p => p.PriceMonthly)
                           .ToList()
            };

            return View(vm);
        }


        [HttpGet("api/sessions/upcoming")]
        public IActionResult UpcomingSessions(int take = 8)
        {
            var now = DateTime.UtcNow;

            var items = _db.ScheduledSessions
                .Include(s => s.SessionType)
                .Include(s => s.Branch).ThenInclude(b => b.City)
                .Where(s => !s.IsCanceled && s.StartAtUtc > now)
                .OrderBy(s => s.StartAtUtc)
                .Take(Math.Clamp(take, 1, 20))
                .Select(s => new
                {
                    id = s.Id,
                    title = s.SessionType.TitleAr,
                    branch = s.Branch.NameAr,
                    city = s.Branch.City.NameAr,
                    start = s.StartAtUtc,
                    duration = s.DurationMin
                })
                .AsEnumerable()
                .Select(x => new
                {
                    x.id,
                    x.title,
                    x.branch,
                    x.city,
                    startLocal = x.start.ToLocalTime().ToString("yyyy/MM/dd HH:mm"),
                    x.duration
                });

            return Json(items);
        }


        public IActionResult Privacy() => View();


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() =>
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
