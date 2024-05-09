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

        public DbSet<ecommerce_temp.Models.Cart> Cart { get; set; } = default!;
        public DbSet<ecommerce_temp.Models.Product> Product { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new CartMap());
            // modelBuilder.ApplyConfiguration(new AdressMap());
            // modelBuilder.ApplyConfiguration(new PaymenMethodMap());
            // modelBuilder.ApplyConfiguration(new AccountMap());
            // modelBuilder.ApplyConfiguration(new AccountTypeMap());
            // modelBuilder.ApplyConfiguration(new CategoryMap());
            // modelBuilder.ApplyConfiguration(new OrderMap());
            // modelBuilder.ApplyConfiguration(new OrderItemMap());
            base.OnModelCreating(modelBuilder);


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
