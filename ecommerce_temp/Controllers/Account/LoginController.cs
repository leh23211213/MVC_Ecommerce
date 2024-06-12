using ecommerce_temp.Data.Models;
using ecommerce_temp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_temp.Controllers.Account
{
    [Area("Account")]
    public class LoginController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LoginController> _logger;

        public LoginController(SignInManager<User> signInManager, ILogger<LoginController> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction("LoginWith2fa", "Login", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToAction("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction();
            }
        }

        [HttpGet]
        public IActionResult LoginWith2fa(string returnUrl = null, bool rememberMe = false)
        {
            // Implement your 2FA logic here
            return View();
        }

        [HttpGet]
        public IActionResult Lockout()
        {
            return View();
        }
    }
}
