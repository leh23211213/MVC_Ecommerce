namespace Web_MVC_Commerce.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using MVC_Ecommerce.Models;
    using Web_MVC_Commerce.Models;
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        public DbSet<User> Users { get; set; }
        // public DbSet<Address> Addresses { get; set; }
        // public DbSet<PaymentMethod> PaymentMethods { get; set; }
        // public DbSet<Account> Accounts { get; set; }
        // public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        // public DbSet<Order> Orders { get; set; }
        // public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration(new UsersMap());
            // modelBuilder.ApplyConfiguration(new AdressMap());
            // modelBuilder.ApplyConfiguration(new PaymenMethodMap());
            // modelBuilder.ApplyConfiguration(new AccountMap());
            // modelBuilder.ApplyConfiguration(new AccountTypeMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new CartMap());
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
