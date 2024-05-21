using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ecommerce_temp.Controllers
{
    [Authorize]
    [Route("Cart")]
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            // TODO: bug userId can not get
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                // Xử lý khi không tìm thấy userId
                return Unauthorized(); // Hoặc chuyển hướng đến trang đăng nhập
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
            var userId = User.GetUserId();
            _cartService.AddToCart(userId, productId, quantity);
            return RedirectToAction("Index");
        }
        [HttpPost("RemoveFromCart")]
        public IActionResult RemoveFromCart(string productId)
        {
            var userId = User.GetUserId();
            _cartService.RemoveFromCart(userId, productId);
            return RedirectToAction("Index");
        }
        [HttpPost("ClearCart")]
        public IActionResult ClearCart()
        {
            var userId = User.GetUserId();
            _cartService.ClearCart(userId);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult IncreaseQuantity(string cartItemId)
        {
            var userId = "user1"; // Ví dụ người dùng
            var updatedCart = _cartService.IncreaseCartItemQuantity(userId, cartItemId);
            return Json(updatedCart);
        }

        [HttpPost]
        public IActionResult DecreaseQuantity(string cartItemId)
        {
            var userId = "user1"; // Ví dụ người dùng
            var updatedCart = _cartService.DecreaseCartItemQuantity(userId, cartItemId);
            return Json(updatedCart);
        }
    }
}