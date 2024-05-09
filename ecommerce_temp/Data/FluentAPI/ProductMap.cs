using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ecommerce_temp.Models;

namespace ecommerce_temp.Data
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(e => e.ProductId);
            builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Price).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(e => e.Description).IsRequired(false);
            builder.Property(e => e.Quantity).IsRequired(false);
            builder.Property(e => e.ImageUrl).HasMaxLength(255).IsRequired(false);
            builder.Property(e => e.Vote).IsRequired(false);
            // Định nghĩa khóa ngoại
            builder.HasOne<Category>()
                   .WithMany()
                   .HasForeignKey(e => e.CategoryId);
        }
    }
}