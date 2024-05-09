// namespace Web_MVC_Commerce.Data
// {
//     using Microsoft.EntityFrameworkCore;
//     using MVC_Ecommerce.Models;
//     using Web_MVC_Commerce.Models;

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