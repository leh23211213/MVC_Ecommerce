namespace MVC_Ecommerce.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MVC_Ecommerce.Models;
    public class UserController : Controller
    {
        private readonly DatabaseEntities _context;
        public UserController(DatabaseEntities context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }
    }
}