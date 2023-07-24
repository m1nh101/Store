using Domain.Entities.Orders;
using Infrastructure.Database.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
  public void Configure(EntityTypeBuilder<Order> builder)
  {
    builder.ToTable("Orders");

    builder.HasKey(e => e.Id);

    builder.Property(e => e.Id)
    .ValueGeneratedOnAdd();

    builder.Property(e => e.UserId)
    .IsRequired(true)
    .HasConversion<KeyConverter>();

    builder.OwnsOne(e => e.Address);

    builder.OwnsMany(e => e.Items, sp =>
    {
      sp.ToTable("OrderItems");

      sp.WithOwner().HasForeignKey("OrderId");
    });

    builder.Ignore(e => e.Events);
  }
}
