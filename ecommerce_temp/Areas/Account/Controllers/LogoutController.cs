using ecommerce_temp.Areas.Account.Models;
using ecommerce_temp.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_temp.Areas.Account.Controllers
{
    [Area("Account")]
    [Route("[Area]/[action]")]
    public class LogoutController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LogoutController> _logger;
        [ActivatorUtilitiesConstructor]
        public LogoutController(SignInManager<User> signInManager, ILogger<LogoutController> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Login", "Account", new { area = "Account" });
            }
        }
    }
}