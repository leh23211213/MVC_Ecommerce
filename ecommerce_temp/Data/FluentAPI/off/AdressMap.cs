// using Microsoft.EntityFrameworkCore;
// using MVC_Ecommerce.Models;

// namespace Web_MVC_Commerce.Data
// {
//     public class AdressMap : IEntityTypeConfiguration<Address>
//     {
//         public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Address> builder)
//         {
//             builder.ToTable("Addresses");
//             builder.HasKey(e => e.AddressId);
//             builder.Property(e => e.AddressLine1).IsRequired();
//             builder.Property(e => e.City).HasMaxLength(50);
//             builder.Property(e => e.State).HasMaxLength(50);
//             builder.Property(e => e.Country).HasMaxLength(50);
//             builder.Property(e => e.PostalCode).HasMaxLength(20);
//             /*
//             builder.HasOne<User>(e => e.User)
//                .WithMany(m => m.Addresses) // Đảm bảo bạn sử dụng tên thuộc tính navigation chính xác
//                .HasForeignKey(e => e.UserId)
//                .OnDelete(DeleteBehavior.Restrict); 
//                */
//         }
//     }
// }