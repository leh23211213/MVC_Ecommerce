
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using ecommerce_temp.Data.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace ecommerce_temp.Areas.Account.Controllers
{
    [Area("Account")]
    [Route("[Area]/[action]")]
    public class ConfirmEmailController : Controller
    {
        private readonly UserManager<User> _userManager;

        public ConfirmEmailController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code, string returnUrl = null)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(Register));
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load user with ID '{userId}'.");
            }
            var decodedCode = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, decodedCode);
            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl ?? Url.Content("~/Product"));
            }
            else
            {
                return View("Error");
            }
        }
    }
}
