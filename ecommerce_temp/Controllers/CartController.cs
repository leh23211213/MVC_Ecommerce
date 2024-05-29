using ecommerce_temp.Data;
using ecommerce_temp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_temp.Controllers
{
    [Authorize]
    [Route("Cart")]
    public class CartController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ecommerce_tempContext _context;
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
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                // Handle when userId is not found
                return Unauthorized(); // Or redirect to the login page
            }

            var cartViewModel = _cartService.GetView(userId);

            if (cartViewModel == null)
            {
                return NotFound();
            }

            return View(cartViewModel);
        }

        [HttpPost("Add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(string productId)
        {
            // var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            // if (product == null)
            // {
            //     return NotFound("{product.Name} Not exits.");
            // }

            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized("User not found");
            }

            _cartService.AddToCart(userId, productId);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> AddToCart()
        {

            return View("Index");
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItems = await _context.CartItems.FindAsync(id);
            if (cartItems != null)
            {
                _context.CartItems.Remove(cartItems);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("");
        }
    }
}
