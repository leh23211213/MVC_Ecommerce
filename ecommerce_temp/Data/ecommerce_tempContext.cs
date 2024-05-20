using Microsoft.EntityFrameworkCore;
using ecommerce_temp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ecommerce_temp.Data
{
    public class ecommerce_tempContext : IdentityDbContext<User>
    {
        public ecommerce_tempContext(DbContextOptions<ecommerce_tempContext> options)
            : base(options)
        {
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new CartItemMap());
            modelBuilder.ApplyConfiguration(new CartMap());
            // modelBuilder.ApplyConfiguration(new PaymenMethodMap());
            // modelBuilder.ApplyConfiguration(new CategoryMap());
            // modelBuilder.ApplyConfiguration(new OrderMap());
            // modelBuilder.ApplyConfiguration(new OrderDetailMap());

            base.OnModelCreating(modelBuilder);
            // SeedData
            modelBuilder.Entity<Product>().HasData(
           new Product { ProductId = "1", Name = "Product 1", Price = 100, ImageUrl = "/images/product1.png" },
           new Product { ProductId = "2", Name = "Product 2", Price = 200, ImageUrl = "/images/product2.png" }
       );

            modelBuilder.Entity<Cart>().HasData(
                new Cart { CartId = "1", UserId = "user1" }
            );

            modelBuilder.Entity<CartItem>().HasData(
                new CartItem { CartItemId = "1", CartId = "1", ProductId = "1", Quantity = 1 },
                new CartItem { CartItemId = "2", CartId = "1", ProductId = "2", Quantity = 2 }
            );

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
        }
    }
}
