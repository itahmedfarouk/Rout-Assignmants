using Demo.DAL.Models.IdentityModels;
using Demo.PL.Utilities;
using Demo.PL.ViewModels.IdentityViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace Demo.PL.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager) : Controller
    {
        ////private readonly UserManager<ApplicationUser> _userManager;

        ////public AccountController(UserManager<ApplicationUser> userManager)
        ////{
        ////    _userManager = userManager;
        ////}

        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userToAdd = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email
            };

            var res = _userManager.CreateAsync(userToAdd, model.Password).Result;

            if (res.Succeeded)
                return RedirectToAction("Login");

            foreach (var error in res.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
        #endregion

        #region Login
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(LogInViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = _userManager.FindByEmailAsync(model.Email).Result;
            if (user is not null)
            {
                var IsCorrectPass = _userManager.CheckPasswordAsync(user, model.Password).Result;
                if (IsCorrectPass)
                {
                    var res = _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false).Result;
                    if (res.IsNotAllowed) ModelState.AddModelError(string.Empty, "Your Account Is Not Allowed");
                    if (res.IsLockedOut) ModelState.AddModelError(string.Empty, "Your Account Is Locked");
                    if (res.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index), "Home");


                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login");
            }
            return View();
        }
        #endregion
        [HttpGet]
        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction(nameof(LogIn));
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();

        }
        [HttpPost]
        public IActionResult SendResetPasswordLink(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(model.Email).Result;
                if (user is not null)
                {
                    //Create Reset Password Link
                    //BaseUrl/Account/ResetPasswordLink?email=ahmedmedo90477@gmail.com
                    var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                    var resetPasswordLink = Url.Action("ResetPasswordLink", "Account",
                        new { email = model.Email, token }, Request.Scheme);



                    // Crate Email
                    var mail = new Email()
                    {
                        To = model.Email,
                        Subject = "Reset Your Password",
                        Body = resetPasswordLink //ToDo
                    };

                    //Send Email
                    var res = EmailSettings.SendEmail(mail);
                    if (res) return RedirectToAction("CheckYourInbox");
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Operation");
            return View(nameof(ForgetPassword), model);

        }


        [HttpGet]
        public IActionResult CheckYourInbox()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPasswordLink(string email, string token)
        {
            
            return View();
        }
    }
}