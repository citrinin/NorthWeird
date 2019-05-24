using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NorthWeird.WebUI.ViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NorthWeird.WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<IdentityUser> _claimsPrincipalFactory;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(
            UserManager<IdentityUser> userManager,
            IUserClaimsPrincipalFactory<IdentityUser> claimsPrincipalFactory,
            SignInManager<IdentityUser> signInManager
            )
        {
            _userManager = userManager;
            _claimsPrincipalFactory = claimsPrincipalFactory;
            _signInManager = signInManager;
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
                var user = await _userManager.FindByEmailAsync(model.UserName);

                if (user == null)
                {
                    user = new IdentityUser(model.UserName);

                    var result = await _userManager.CreateAsync(user, model.Password);
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
                    RedirectToAction("Index", "Product");
                }

                ModelState.AddModelError("", "Invalid username or password");
            }

            return View();
        }
    }
}
