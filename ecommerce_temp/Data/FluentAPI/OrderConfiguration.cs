using Microsoft.EntityFrameworkCore;
using ecommerce_temp.Models;
namespace ecommerce_temp.Data
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(e => e.OrderId);
            builder.Property(e => e.OrderDate).HasColumnType("datetime2").IsRequired();
            builder.Property(e => e.TotalPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(o => o.Status).HasConversion<string>();

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .IsRequired();
            builder
                    .HasMany(o => o.OrderDetails)
                    .WithOne(od => od.Order)
                    .HasForeignKey(od => od.OrderId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}