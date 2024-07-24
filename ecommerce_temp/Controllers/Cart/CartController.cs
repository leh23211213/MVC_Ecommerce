using ecommerce_temp.Data;
using ecommerce_temp.Data.Models;
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
        private readonly ILogger<ProductController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly ecommerce_tempContext _context;
        private readonly CartService _cartService;

        [ActivatorUtilitiesConstructor] // BUG: MULTIple contructors add <~
        public CartController(ecommerce_tempContext context, UserManager<User> userManager, CartService cartService, ILogger<ProductController> logger)
        {
            _cartService = cartService;
            _userManager = userManager;
            _logger = logger;
            _context = context;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
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
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            if (product == null)
            {
                return NotFound(new { message = "Product not found." });
            }

            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized("User not found");
            }

            try
            {
                await _cartService.Add(userId, productId); // Ensure AddAsync is awaited
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // TODO : clean code
        // POST: Classes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized("User not found");
            }

            var cart = _context.Carts
                        .Include(c => c.CartItems)
                        .FirstOrDefault(c => c.UserId == userId);
            if (cart != null)
            {
                var cartItem = cart.CartItems.FirstOrDefault(ci => ci.CartItemId == id);
                if (cartItem != null)
                {
                    _context.CartItems.Remove(cartItem);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("");
        }

        [HttpPost("DeleteAll")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAll()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized("User not found");
            }

            var cart = _context.Carts
                            .Include(c => c.CartItems)
                            .FirstOrDefault(c => c.UserId == userId);
            if (cart != null)
            {

                _context.CartItems.RemoveRange(cart.CartItems);
                _context.SaveChanges();
            }
            return RedirectToAction("");
        }
    }
}
