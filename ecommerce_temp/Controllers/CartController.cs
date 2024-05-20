using System.Security.Claims;
using ecommerce_temp.Models;
using Microsoft.AspNetCore.Mvc;
namespace ecommerce_temp.Controllers
{
    // [Route("[controller]")]
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = "user1"; // Ví dụ người dùng
            var cart = _cartService.GetCartByUserId(userId);
            if (cart == null)
            {
                // Nếu cart null, tạo một đối tượng mới
                cart = new Cart
                {
                    CartItems = new List<CartItem>()
                };
            }
            var cartViewModel = new CartViewModel
            {
                CartItems = cart.CartItems.Select(item => new CartItemViewModel
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product.Name,
                    ImageUrl = item.Product.ImageUrl,
                    Price = item.Product.Price,
                    Quantity = item.Quantity
                }).ToList(),
            };

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

        public IActionResult RemoveFromCart(string productId)
        {
            var userId = User.GetUserId();
            _cartService.RemoveFromCart(userId, productId);
            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
            var userId = User.GetUserId();
            _cartService.ClearCart(userId);
            return RedirectToAction("Index");
        }
    }
}