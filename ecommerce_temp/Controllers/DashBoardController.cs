using ecommerce_temp.Data.Models;
using ecommerce_temp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ecommerce_temp.Controllers
{
    [Route("[controller]")]
    public class DashBoardController : Controller
    {
        private readonly UserManager<User> _userManager;

        public DashBoardController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index(string returnUrl = null)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var model = new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                // FullName = user.FullName, // Assuming you have this property in your User model
                //  DateRegistered = user.DateRegistered // Assuming you have this property in your User model
                // ...
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
