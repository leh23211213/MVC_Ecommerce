namespace Web_MVC_Commerce.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MVC_Ecommerce.Models;
    public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            builder.HasKey(e => e.OrderItemId);
            builder.Property(e => e.Quantity).IsRequired();
            builder.Property(e => e.Price).HasColumnType("decimal(18,2)").IsRequired();
            // Định nghĩa khóa ngoại
            builder.HasOne<Order>()
                   .WithMany()
                   .HasForeignKey(e => e.OrderId);
            builder.HasOne<Product>()
                   .WithMany()
                   .HasForeignKey(e => e.ProductId);
        }
    }
}