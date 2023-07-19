using Domain.Entities.Products;
using Infrastructure.Database.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public sealed class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
  public void Configure(EntityTypeBuilder<Sale> builder)
  {
    builder.ToTable("Sales");

    builder.HasKey(e => e.Id);

    builder.Property(e => e.Id)
      .HasConversion<KeyConverter>();
  }
}
