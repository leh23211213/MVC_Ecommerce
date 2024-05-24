// public class AccountController : Controller
// {
//     private readonly UserManager<ApplicationUser> _userManager;
//     private readonly SignInManager<ApplicationUser> _signInManager;

//     public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
//     {
//         _userManager = userManager;
//         _signInManager = signInManager;
//     }

//     [HttpPost]
//     public async Task<IActionResult> Login(LoginViewModel model)
//     {
//         if (ModelState.IsValid)
//         {
//             var user = await _userManager.FindByNameAsync(model.Username);
//             if (user != null)
//             {
//                 var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
//                 if (result.Succeeded)
//                 {
//                     // User authenticated
//                     return RedirectToAction("Index", "Home");
//                 }
//             }

//             ModelState.AddModelError("", "Invalid login attempt.");
//         }

//         return View(model);
//     }

//     public async Task<IActionResult> Logout()
//     {
//         await _signInManager.SignOutAsync();
//         return RedirectToAction("Index", "Home");
//     }
// }
