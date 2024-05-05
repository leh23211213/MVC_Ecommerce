using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC_Ecommerce.Models;
namespace Web_MVC_Commerce.Data
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(e => e.ProductId);
            builder.Property(e => e.ProductId).ValueGeneratedOnAdd(); // Thiết lập Id thành identity
            builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Price).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(e => e.Description).IsRequired(false);
            builder.Property(e => e.Quantity).IsRequired(false);
            builder.Property(e => e.Vote).IsRequired(false);
            builder.Property(e => e.ImageUrl).HasMaxLength(255).IsRequired(false);
            // Định nghĩa khóa ngoại
            builder.HasOne<Category>()
                   .WithMany()
                   .HasForeignKey(e => e.CategoryId);
        }
    }
}