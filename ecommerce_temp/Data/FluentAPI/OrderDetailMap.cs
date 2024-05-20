// namespace ecommerce_temp.Data
// {
//     using Microsoft.EntityFrameworkCore;
//     using Microsoft.EntityFrameworkCore.Metadata.Builders;
//     using ecommerce_temp.Models;
//     public class OrderDetailMap : IEntityTypeConfiguration<OrderDetail>
//     {
//         public void Configure(EntityTypeBuilder<OrderDetail> builder)
//         {
//             builder.ToTable("OrderItems");
//             builder.HasKey(e => e.OrderId);
//             builder.Property(e => e.Quantity).IsRequired();
//             // builder.Property(e => e.Price).HasColumnType("decimal(18,2)").IsRequired();

//             // Định nghĩa khóa ngoại
//             builder.HasOne<Order>()
//                    .WithMany()
//                    .HasForeignKey(e => e.OrderId);
//             builder.HasOne<Product>()
//                    .WithMany()
//                    .HasForeignKey(e => e.ProductId);
//         }
//     }
// }