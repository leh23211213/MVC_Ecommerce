// namespace ecommerce_temp.Data
// {
//     using Microsoft.EntityFrameworkCore;
//     using ecommerce_temp.Models;

//     public class OrderMap : IEntityTypeConfiguration<Order>
//     {
//         public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Order> builder)
//         {
//             builder.ToTable("Orders");
//             builder.HasKey(e => e.OrderId);
//             builder.Property(e => e.OrderDate).HasColumnType("datetime2").IsRequired();
//             builder.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)").IsRequired();
//             builder.Property(e => e.Status).HasMaxLength(20).IsRequired();
//             builder.HasOne<User>()
//                    .WithMany()
//                    .HasForeignKey(e => e.UserId);
//         }
//     }
// }