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
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            var productViewModels = products.Select(p => new ProductViewModel
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Price = p.Price,
                Description = p.Description,
                ImageUrl = p.ImageUrl
            }).ToList();
            return View(productViewModels);
        }

        // GET: Products/Details/5
        [HttpGet("Details/{id}")]
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

            return View(product);
        }

        [HttpGet("Search/{item}")]
        public async Task<IActionResult> Search(string item)
        {
            if (string.IsNullOrWhiteSpace(item))
            {
                ViewBag.Message = "Please enter a valid keyword (at least 1 characters)";
                return View("Index", new List<ProductViewModel>());
            }

            var productViewModels = await SearchProductsAsync(item);

            if (productViewModels == null || !productViewModels.Any())
            {
                ViewBag.Message = "No products found";
            }

            return View("Index", productViewModels);
        }

        private async Task<List<ProductViewModel>> SearchProductsAsync(string search)
        {
            var input = search.ToLower();
            var products = await _context.Products
                .Where(p => p.ProductName.ToLower().Contains(input))
                .Select(p => new ProductViewModel
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl
                })
                .ToListAsync();

            return products;
        }

        private bool ProductExists(string id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
