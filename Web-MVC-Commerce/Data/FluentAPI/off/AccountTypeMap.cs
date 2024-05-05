// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using MVC_Ecommerce.Models;

// namespace Web_MVC_Commerce.Data
// {
//     public class AccountTypeMap : IEntityTypeConfiguration<AccountType>
//     {
//         public void Configure(EntityTypeBuilder<AccountType> builder)
//         {
//             builder.ToTable("AccountTypes");
//             builder.HasKey(e => e.AccountTypeId);
//             builder.Property(e => e.TypeName).HasMaxLength(20).IsRequired();
//         }
//     }
// }