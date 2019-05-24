using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using NorthWeird.Infrastructure.Mailing;
using NorthWeird.WebUI.ViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NorthWeird.WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<IdentityUser> _claimsPrincipalFactory;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public AuthController(
            UserManager<IdentityUser> userManager,
            IUserClaimsPrincipalFactory<IdentityUser> claimsPrincipalFactory,
            SignInManager<IdentityUser> signInManager,
            IEmailSender emailSender
            )
        {
            _userManager = userManager;
            _claimsPrincipalFactory = claimsPrincipalFactory;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpGet]
        public  IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                var userByEmail = await _userManager.FindByEmailAsync(model.Email);

                if (user == null && userByEmail == null)
                {
                    user = new IdentityUser(model.UserName) { Email = model.Email };

                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var confirmationUrl = Url.Action("ConfirmEmailAddress", "Auth",
                            new { token, email = user.Email }, Request.Scheme);

                        await _emailSender.SendEmailAsync(
                            model.Email,
                            "Please, confirm your email",
                            MessageHelper.GetEmailConfirmationMessage(confirmationUrl) 
                        );
                    }
                }
                else
                {
                    ModelState.AddModelError("", "This username or email is already taken");
                    return View();
                }

                return RedirectToAction("Index", "Product");
            }

            return View();
        }

        [Authorize]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var signInResult =
                    await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Product");
                }

                ModelState.AddModelError("", "Invalid username or password");
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {

            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user!=null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetUrl = Url.Action("ResetPassword", "Auth", new { token, email = user.Email},
                        Request.Scheme);

                    await _emailSender.SendEmailAsync(
                        model.Email, 
                        "You forgot your password", 
                        MessageHelper.GetEmailResetPassMessage(resetUrl)
                        );
                }
                else
                {
                    await _emailSender.SendEmailAsync(
                        model.Email, 
                        "Pass restore",
                        MessageHelper.GetEmailDeniedResetPassMessage()
                        );

                }

                return View("Success");
            }

            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        return View();
                    }

                    return View("Success");
                }

                ModelState.AddModelError("", "Invalid request");
            }

            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmailAddress(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);

                if (result.Succeeded)
                {
                    return View("Success");
                }
            }

            return View("Error");
        }
    }
}
