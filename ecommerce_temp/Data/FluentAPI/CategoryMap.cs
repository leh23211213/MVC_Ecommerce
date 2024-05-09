using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ecommerce_temp.Models;

namespace Web_MVC_Commerce.Data
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(e => e.CategoryId);
            builder.Property(e => e.CategoryName).HasMaxLength(100).IsRequired();
        }
    }
}