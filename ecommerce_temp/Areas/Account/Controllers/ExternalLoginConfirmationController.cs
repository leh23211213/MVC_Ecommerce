using ecommerce_temp.Data.Models;
using ecommerce_temp.Areas.Account.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_temp.Areas.Account.Controllers
{
    [Area("Account")]
    [Route("[Area]/[action]")]
    public class ExternalLoginConfirmationController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ExternalLoginController> _logger;
        private readonly IUserStore<User> _userStore;
        [ActivatorUtilitiesConstructor]
        public ExternalLoginConfirmationController(SignInManager<User> signInManager, UserManager<User> userManager, ILogger<ExternalLoginController> logger, IUserStore<User> userStore)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _userStore = userStore;
        }

        [HttpGet]
        public IActionResult ExternalLoginConfirmation(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/Product");
            if (ModelState.IsValid)
            {
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return RedirectToAction(nameof(LoginController.Login), "Login");
                }

                var user = new User { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        private IUserEmailStore<User> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<User>)_userStore;
        }
    }
}
