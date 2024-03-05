namespace MVC_Ecommerce.Models
{
    using Microsoft.EntityFrameworkCore;
    public class DatabaseEntities : DbContext
    {
        public DatabaseEntities(DbContextOptions<DatabaseEntities> options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}