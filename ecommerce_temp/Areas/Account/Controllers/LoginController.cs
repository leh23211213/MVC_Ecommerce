using ecommerce_temp.Areas.Account.Models;
using ecommerce_temp.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace ecommerce_temp.Areas.Account.Controllers
{
    [Area("Account")]
    [Route("[Area]/[action]")]
    [Authorize]
    public class LoginController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LoginController> _logger;
        private readonly IConnectionMultiplexer _redis;

        [ActivatorUtilitiesConstructor]
        public LoginController(SignInManager<User> signInManager, ILogger<LoginController> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            ViewData["ReturnUrl"] = returnUrl;
            returnUrl ??= Url.Content("~/Product");

            if (ModelState.IsValid)
            {
                var database = _redis.GetDatabase();
                string rateLimitKey = $"rl:{model.Email}";
                string lockoutKey = $"lockout:{model.Email}";
                var lockoutValue = await database.StringGetAsync(lockoutKey);
                if (lockoutValue.HasValue && lockoutValue == "true")
                {
                    ModelState.AddModelError(string.Empty, "Too many login attempts. Please try again later.");
                    return View("Index", "Home");
                }

                var attempt = await database.StringIncrementAsync(rateLimitKey);
                if (attempt == 1)
                {
                    // Đặt thời gian hết hạn cho khóa rate limit
                    await database.KeyExpireAsync(rateLimitKey, TimeSpan.FromMinutes(1));
                }

                if (attempt == 5)
                {
                    // Khóa người dùng trong 5 phút
                    await database.StringSetAsync(lockoutKey, "true", TimeSpan.FromMinutes(5));
                    ModelState.AddModelError(string.Empty, "Too many login attempts. Please try again later.");
                    return View("Index", "Home");
                }

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction("LoginWith2fa", "Account", new { ReturnUrl = returnUrl });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToAction("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                    return View(model);
                }
            }

            return View(model);
        }
    }
}
