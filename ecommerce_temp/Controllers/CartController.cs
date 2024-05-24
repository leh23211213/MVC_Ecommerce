using ecommerce_temp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_temp.Controllers
{
    [Authorize]
    [Route("Cart")]
    public class CartController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly CartService _cartService;


        [ActivatorUtilitiesConstructor] // BUG: MULTIple contructors add <~
        public CartController(UserManager<User> userManager, CartService cartService)
        {
            _cartService = cartService;
            _userManager = userManager;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            // TODO: bug userId can not get
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                // Handle when userId is not found
                return Unauthorized(); // Or redirect to the login page
            }

            var cartViewModel = _cartService.GetCartByUserId(userId);

            if (cartViewModel == null)
            {
                return NotFound();
            }

            return View(cartViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(string productId, int quantity)
        {
            var userId = _userManager.GetUserId(User);
            _cartService.AddToCart(userId, productId, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost("RemoveFromCart")]
        public IActionResult RemoveFromCart(string productId)
        {
            var userId = _userManager.GetUserId(User);
            _cartService.RemoveFromCart(userId, productId);
            return RedirectToAction("Index");
        }

        [HttpPost("ClearCart")]
        public IActionResult ClearCart()
        {
            var userId = _userManager.GetUserId(User);
            _cartService.ClearCart(userId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult IncreaseQuantity(string cartItemId)
        {
            var userId = _userManager.GetUserId(User);
            var updatedCart = _cartService.IncreaseCartItemQuantity(userId, cartItemId);
            return Json(updatedCart);
        }

        [HttpPost]
        public IActionResult DecreaseQuantity(string cartItemId)
        {
            var userId = _userManager.GetUserId(User);
            var updatedCart = _cartService.DecreaseCartItemQuantity(userId, cartItemId);
            return Json(updatedCart);
        }
    }
}
