using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecommerce_temp.Data;
using ecommerce_temp.Data.Models;
using ecommerce_temp.Models.Product;

namespace ecommerce_temp.Controllers
{
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ecommerce_tempContext _context;

        public ProductController(ILogger<ProductController> logger, ecommerce_tempContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: Products
        [HttpGet("")]
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 6;

            var products = await _context.Products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalProduct = await _context.Products.CountAsync();
            var totalPages = (int)Math.Ceiling(totalProduct / (double)pageSize);


            var productViewModels = products.Select(p => new ProductViewModel
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Price = p.Price,
                Description = p.Description,
                ImageUrl = p.ImageUrl
            }).ToList();

            var viewModel = new ProductListViewModel
            {
                Products = productViewModels,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(viewModel);
        }

        // GET: Products/Details/5
        [HttpGet("Details")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductViewModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price
            };

            return View(viewModel);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search(string item, int page = 1)
        {
            // TODO : lỗi null khi không nhập giá trị nào hêt.
            if (string.IsNullOrEmpty(item) || item == null || item.Length == 0)
            {
                ViewBag.Message = "Please enter a valid keyword (at least 1 characters)";
                return View("Index", new ProductListViewModel());
            }

            var productListViewModel = await SearchProductsAsync(item, page);
            if (productListViewModel == null || !productListViewModel.Products.Any())
            {
                ViewBag.Message = "No products found";
            }

            return View("Index", productListViewModel);
        }

        private async Task<ProductListViewModel> SearchProductsAsync(string search, int page)
        {
            int pageSize = 6;
            var input = search.ToLower();
            var products = await _context.Products
                .Where(p => p.ProductName.ToLower().Contains(input))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductViewModel
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl
                })
                .ToListAsync();

            var totalProducts = await _context.Products.CountAsync(p => p.ProductName.ToLower().Contains(input));
            var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

            return new ProductListViewModel
            {
                Products = products,
                CurrentPage = page,
                TotalPages = totalPages
            };
        }
    }
}
