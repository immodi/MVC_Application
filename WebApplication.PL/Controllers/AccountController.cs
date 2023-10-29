using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplicaiton.DAL.Models;
using WebApplication.PL.Helpers;
using WebApplication.PL.ViewModels;

namespace WebApplication.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Email = model.Email,
                    UserName = model.Email.Split('@')[0],
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    IsAgree = model.IsAgree
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }
            return View(model); 
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RemeberMe, false);
                        if (result.Succeeded)
                        { 
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    ModelState.AddModelError(string.Empty, "Password is invalid");
                }
                ModelState.AddModelError(string.Empty, "Email is invalid");
            }

            return View(model);
        }

        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var Token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var PasswordResetLink = Url.Action("ResetPassword", "Account", new { email = user.Email, token = Token }, Request.Scheme);
                    var email = new Email()
                    {
                        Subject = "Reset Password",
                        Receiver = user.Email,
                        Body = PasswordResetLink,
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
                ModelState.AddModelError(string.Empty, "Email is not vaild!");
            }
            return View();
        }

        public IActionResult CheckYourInbox()
        {
            return View();
        }

        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                string email = (string) TempData["email"];
                string token = (string) TempData["token"];
                var user = await _userManager.FindByEmailAsync(email);

                var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
    }
}
