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
               new Product { ProductId = "IP13MiniBK128GB", Name = "iPhone 13 Mini", Price = 999, ImageUrl = "~/lib/image/SmartPhone/Iphone/IP13-Mini-BK-128GB.png" },
               new Product { ProductId = "IP13MiniPK128GB", Name = "iPhone 13 Mini", Price = 999, ImageUrl = "~/lib/image/SmartPhone/Iphone/IP13-Mini-PK-128GB.png" }
           );

            modelBuilder.Entity<CartItem>().HasData(
                new CartItem { CartItemId = 1, ProductId = "IP13MiniBK128GB", Quantity = 1, CartId = "1" },
                new CartItem { CartItemId = 2, ProductId = "IP13MiniPK128GB", Quantity = 1, CartId = "1" }
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

        // public override int SaveChanges()
        // {
        //     var entries = ChangeTracker.Entries<User>().Where(e => e.State == EntityState.Added).ToList();

        //     foreach (var entry in entries)
        //     {
        //         var user = entry.Entity;
        //         Carts.Add(new Cart
        //         {
        //             UserId = user.Id,
        //             DateCreated = DateTime.Now
        //         });
        //     }

        //     return base.SaveChanges();
        // }

        // public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        // {
        //     var entries = ChangeTracker.Entries<User>().Where(e => e.State == EntityState.Added).ToList();

        //     foreach (var entry in entries)
        //     {
        //         var user = entry.Entity;
        //         Carts.Add(new Cart
        //         {
        //             UserId = user.Id,
        //             DateCreated = DateTime.Now
        //         });
        //     }

        //     return await base.SaveChangesAsync(cancellationToken);
        // }
    }
}
