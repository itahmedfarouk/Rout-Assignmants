using System.Linq;
using System.Threading.Tasks;
using GymCRM.Data;
using GymCRM.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace GymCRM.Controllers
{
    [Route("account")]
    public class AccountAuthController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signIn;
        private readonly UserManager<ApplicationUser> _users;
        private readonly GymCRMContext _db;

        public AccountAuthController(SignInManager<ApplicationUser> signIn, UserManager<ApplicationUser> users, GymCRMContext db)
        {
            _signIn = signIn;
            _users = users;
            _db = db;
        }

        [HttpGet("login")]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View("~/Views/Account/Login.cshtml");
        }

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password, string? returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("", "ادخل اسم المستخدم أو البريد وكلمة المرور.");
                return View("~/Views/Account/Login.cshtml");
            }

            var user = await _users.FindByNameAsync(username) ?? await _users.FindByEmailAsync(username);
            if (user == null)
            {
                ModelState.AddModelError("", "بيانات الدخول غير صحيحة.");
                return View("~/Views/Account/Login.cshtml");
            }

            var res = await _signIn.PasswordSignInAsync(user, password, isPersistent: true, lockoutOnFailure: false);
            if (!res.Succeeded)
            {
                ModelState.AddModelError("", "بيانات الدخول غير صحيحة.");
                return View("~/Views/Account/Login.cshtml");
            }

            var customer = await _db.Customers.FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (customer == null && !string.IsNullOrWhiteSpace(user.Email))
            {
                customer = await _db.Customers.FirstOrDefaultAsync(c => c.Email == user.Email);
                if (customer != null)
                {
                    customer.UserId = user.Id;
                    await _db.SaveChangesAsync();
                }
            }

            if (customer == null)
            {
                customer = new Customer
                {
                    FullName = string.IsNullOrWhiteSpace(user.UserName)
                        ? (user.Email ?? "عميل جديد")
                        : user.UserName!,
                    Email = user.Email ?? "",
                    Phone = user.PhoneNumber ?? "",
                    Gender = Gender.Male, 
                    UserId = user.Id
                };
                _db.Customers.Add(customer);
                await _db.SaveChangesAsync();
            }


            HttpContext.Session.SetInt32("GymCRM.CurrentCustomerId", customer.Id);


            var lastSubId = _db.Subscriptions
                .Where(s => s.CustomerId == customer.Id)
                .OrderByDescending(s => s.Id)
                .Select(s => (int?)s.Id)
                .FirstOrDefault();

            if (lastSubId.HasValue)
                HttpContext.Session.SetInt32("GymCRM.LastSubscriptionId", lastSubId.Value);

            return Redirect(returnUrl ?? $"/account/dashboard?id={customer.Id}");
        }


        [HttpPost("logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signIn.SignOutAsync();

            HttpContext.Session.Remove("GymCRM.CurrentCustomerId");
            HttpContext.Session.Remove("GymCRM.LastSubscriptionId");

            return Redirect("/join/step1");
        }
    }
}
