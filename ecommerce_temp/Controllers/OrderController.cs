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
                    ProductName = od.Product != null ? od.Product.ProductName : null // Kiểm tra null an toàn cho Product
                }).ToList() : new List<OrderDetailViewModel>() // Tránh truy cập vào danh sách null
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

            try
            {
                var order = new Order
                {
                    UserId = userId,
                    CustomerName = "Customer Name", // Thay bằng thông tin khách hàng thực tế
                    Address = "Customer Address",   // Thay bằng thông tin khách hàng thực tế
                    City = "Customer City",         // Thay bằng thông tin khách hàng thực tế
                    Country = "Customer Country",   // Thay bằng thông tin khách hàng thực tế
                    TotalPrice = cart.CartItems.Sum(item => item.Quantity * item.Product.Price), // Tính tổng giá tiền của đơn hàng
                    Status = OrderStatus.Processing, // Trạng thái đơn hàng ban đầu
                    OrderDate = DateTime.Now         // Ngày đặt hàng
                };
                // TODO TODO: order có vấn đề không vào orderDetail
                _context.Orders.Add(order);
                _context.SaveChanges();

                foreach (var cartItem in cart.CartItems)
                {
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
                _context.CartItems.RemoveRange(cart.CartItems);
                _context.Carts.Remove(cart);

                _context.SaveChanges();
                return RedirectToAction("");
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có lỗi trong quá trình xử lý đơn hàng
                // Ví dụ: ghi log lỗi, hiển thị thông báo lỗi cho người dùng, v.v.
                ViewBag.ErrorMessage = "Error processing order: " + ex.Message;
                return View("Error"); // Chuyển hướng đến trang lỗi
            }
        }

        public ActionResult Details(int id)
        {
            var order = _context.Orders
           .Include(o => o.OrderDetails)
               .ThenInclude(od => od.Product)
           .FirstOrDefault(o => o.OrderId == id);

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