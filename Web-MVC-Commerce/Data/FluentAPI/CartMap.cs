
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC_Ecommerce.Models;

namespace Web_MVC_Commerce.Data
{
    public class CartMap : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");
            builder.HasKey(e => e.CartId);
            // Foreign Key defination
            builder.HasOne<Users>()
                   .WithMany()
                   .HasForeignKey(e => e.UserId);
            builder.HasOne<Product>()
                   .WithMany()
                   .HasForeignKey(e => e.ProductId);
        }
    }
}