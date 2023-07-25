using Domain.Entities.Products;
using Infrastructure.Database.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public sealed class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
{
  public void Configure(EntityTypeBuilder<Product> builder)
  {
    builder.ToTable("Products");

    builder.HasIndex(e => new { e.Name, e.Brand });

    builder.HasKey(e => e.Id);

    builder.Property(e => e.Id)
      .HasConversion<KeyConverter>();

    builder.Property(e => e.Name)
      .HasColumnType("nvarchar(500)")
      .HasMaxLength(500);

    builder.Property(e => e.Brand)
      .HasColumnType("nvarchar(218)")
      .HasMaxLength(218);

    builder.HasOne(e => e.Sale)
      .WithMany(e => e.Products)
      .HasForeignKey(e => e.SaleId)
      .IsRequired(false);

    builder.Property(e => e.Price)
      .HasField("_price");

    builder.OwnsMany(e => e.Images, sp =>
    {
      sp.ToTable("Images");

      sp.Property(e => e.Url)
        .HasColumnType("varchar(500)")
        .HasMaxLength(500);

      sp.WithOwner().HasForeignKey("ProductId");
    });
  }
}
