using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecommerce_temp.Data;
using ecommerce_temp.Models;

namespace ecommerce_temp.Controllers
{
    [Route("Product")]
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
            return View(products);
        }

        // GET: Products/Details/5
        [HttpGet("Details")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found.", id);
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
        // GET: Products/Edit/5
        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found.", id);
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ProductId,Name,Description,ImageUrl,Price,Quantity,CategoryId,Vote")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        private bool ProductExists(string id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
