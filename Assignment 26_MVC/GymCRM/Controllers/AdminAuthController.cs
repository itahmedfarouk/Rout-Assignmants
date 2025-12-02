using System.Threading.Tasks;
using GymCRM.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymCRM.Controllers
{
    [AllowAnonymous]
    [Route("admin")]
    public class AdminAuthController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signIn;
        private readonly UserManager<ApplicationUser> _users;

        public AdminAuthController(SignInManager<ApplicationUser> signIn, UserManager<ApplicationUser> users)
        {
            _signIn = signIn; _users = users;
        }

        [HttpGet("login")]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View("~/Views/Admin/Login.cshtml");
        }

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password, string? returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError(string.Empty, "ادخل اسم المستخدم/البريد وكلمة المرور.");
                return View("~/Views/Admin/Login.cshtml");
            }

            var user = await _users.FindByNameAsync(username) ?? await _users.FindByEmailAsync(username);
            if (user == null || !await _users.IsInRoleAsync(user, "Admin"))
            {
                ModelState.AddModelError(string.Empty, "غير مسموح. هذا الحساب ليس أدمن.");
                return View("~/Views/Admin/Login.cshtml");
            }

            var res = await _signIn.PasswordSignInAsync(user, password, isPersistent: true, lockoutOnFailure: false);
            if (!res.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "بيانات الدخول غير صحيحة.");
                return View("~/Views/Admin/Login.cshtml");
            }

            return Redirect(returnUrl ?? "/admin/dashboard");
        }

        [HttpPost("logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signIn.SignOutAsync();
            return Redirect("/");
        }
    }
}
