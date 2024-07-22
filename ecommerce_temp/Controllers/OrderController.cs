using Microsoft.AspNetCore.Mvc;
using ecommerce_temp.Data;
using ecommerce_temp.Models.Order;
using ecommerce_temp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ecommerce_temp.Data.Models;
using Microsoft.EntityFrameworkCore;
using ecommerce_temp.Enums;

namespace ecommerce_temp.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly ecommerce_tempContext _context;
        private readonly UserManager<User> _userManager;
        public OrderController(ecommerce_tempContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Orders
        [HttpGet("")]
        public ActionResult Index()
        {
            var orders = _context.Orders.ToList();

            if (orders == null)
            {
                return View();
            }

            var orderViewModels = orders.Select(o => new OrderViewModel
            {
                UserId = o.UserId,
                OrderId = o.OrderId,
                CustomerName = o.CustomerName,
                Address = o.Address,
                City = o.City,
                Country = o.Country,
                TotalPrice = o.TotalPrice,
                Status = o.Status,
                OrderDate = o.OrderDate,
                OrderDetails = o.OrderDetails != null ? o.OrderDetails.Select(od => new OrderDetailViewModel
                {
                    OrderDetailId = od.OrderDetailId,
                    OrderId = od.OrderId,
                    ProductId = od.ProductId,
                    Quantity = od.Quantity,
                    Price = od.Price,
                    Status = od.Status,
                    ProductName = od.Product != null ? od.Product.ProductName : null
                }).ToList() : new List<OrderDetailViewModel>()
            }).ToList();

            return View(orderViewModels);
        }

        // Action để mua hàng từ giỏ hàng
        [HttpPost("Buy")]
        public async Task<IActionResult> Buy()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var cart = _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefault(c => c.UserId == userId);

            if (cart.CartItems.Count == 0)
            {
                return View();
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Check if cart is null
                    if (cart == null)
                    {
                        throw new Exception("Cart is null.");
                    }

                    // Check if cart items are null or empty
                    if (cart.CartItems == null || !cart.CartItems.Any())
                    {
                        throw new Exception("Cart items are null or empty.");
                    }

                    // Create and add the order
                    var order = new Order
                    {
                        UserId = userId,
                        CustomerName = "Customer Name", // Replace with actual customer info
                        Address = "Customer Address",   // Replace with actual customer info
                        City = "Customer City",         // Replace with actual customer info
                        Country = "Customer Country",   // Replace with actual customer info
                        TotalPrice = cart.CartItems.Sum(item => item.Quantity * item.Product.Price), // Calculate total price
                        Status = OrderStatus.Processing, // Initial order status
                        OrderDate = DateTime.Now         // Order date
                    };

                    _context.Orders.Add(order);
                    _context.SaveChanges(); // Save order to generate OrderId

                    // Check if order is successfully saved and OrderId is generated
                    if (order.OrderId <= 0)
                    {
                        throw new Exception("OrderId not generated.");
                    }

                    // Add order details
                    foreach (var cartItem in cart.CartItems)
                    {
                        // Check if cartItem or cartItem.Product is null
                        if (cartItem == null)
                        {
                            throw new Exception("Cart item is null.");
                        }

                        if (cartItem.Product == null)
                        {
                            throw new Exception($"Product is null for CartItem with ProductId {cartItem.ProductId}.");
                        }

                        var orderDetail = new OrderDetail
                        {
                            OrderId = order.OrderId,
                            ProductId = cartItem.ProductId,
                            Quantity = cartItem.Quantity,
                            Price = cartItem.Product.Price,
                            Status = OrderDetailStatus.Pending
                        };

                        _context.OrderDetails.Add(orderDetail);
                    }

                    // Remove cart items and cart
                    _context.CartItems.RemoveRange(cart.CartItems);
                    _context.Carts.Remove(cart);

                    _context.SaveChanges(); // Save changes for order details and cart removal

                    transaction.Commit(); // Commit transaction

                    return RedirectToAction("OrderSuccess"); // Redirect to success page or appropriate action
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Rollback transaction

                    // Log the exception details
                    Console.WriteLine($"Error processing order: {ex.Message}");
                    Console.WriteLine(ex.StackTrace);

                    // Set error message and return error view
                    ViewBag.ErrorMessage = "Error processing order: " + ex.Message;
                    return View("Error"); // Redirect to error page
                }
            }
        }

        public ActionResult Details(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            var OrderDetails = order.OrderDetails.Select(od => new OrderDetailViewModel
            {
                OrderDetailId = od.OrderDetailId,
                OrderId = od.OrderId,
                ProductId = od.ProductId,
                Quantity = od.Quantity,
                Price = od.Price,
                Status = od.Status,
                ProductName = od.Product.ProductName
            }).ToList();

            return View(OrderDetails);
        }
    }
}