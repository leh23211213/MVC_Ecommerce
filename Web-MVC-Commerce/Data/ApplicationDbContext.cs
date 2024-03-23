namespace MVC_Ecommerce.Data
{
    using Microsoft.EntityFrameworkCore;
    using MVC_Ecommerce.Models;
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Address> Address { get; set; } = default!;
    }
}