using ecommerce_temp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce_temp.Data
{
    public class BrandMap : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("Brands");
            builder.Property(b => b.BrandId).ValueGeneratedOnAdd();
            builder.Property(b => b.BrandName).HasMaxLength(100).IsRequired();
            builder.Property(b => b.ImageUrl).IsRequired(false);
        }
    }
}