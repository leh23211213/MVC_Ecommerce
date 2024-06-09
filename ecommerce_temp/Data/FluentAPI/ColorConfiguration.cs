using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ecommerce_temp.Models;

public class ColorConfiguration : IEntityTypeConfiguration<Color>
{
    public void Configure(EntityTypeBuilder<Color> builder)
    {
        builder.HasKey(c => c.ColorId);
        builder.Property(c => c.ColorName).IsRequired().HasMaxLength(50);

        builder.HasMany(c => c.ProductDetails)
               .WithOne(pd => pd.Color)
               .HasForeignKey(pd => pd.ColorId);
    }
}
