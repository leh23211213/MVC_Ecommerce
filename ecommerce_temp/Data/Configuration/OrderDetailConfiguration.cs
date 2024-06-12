using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ecommerce_temp.Data.Models;
namespace ecommerce_temp.Data.Configuration
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderItems");
            builder.HasKey(e => e.OrderDetailId);
            builder.Property(e => e.Quantity).IsRequired();
            builder.Property(e => e.Price).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(od => od.Status).HasConversion<string>();

            builder.HasOne<Order>()
                    .WithMany(o => o.OrderDetails)
                    .HasForeignKey(od => od.OrderId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Product>()
                    .WithMany()
                    .HasForeignKey(e => e.ProductId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}