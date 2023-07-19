using Domain.Entities.Users;
using Infrastructure.Database.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public sealed class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable("Users");

    builder.HasKey(e => e.Id);

    builder.Property(e => e.Id)
      .HasConversion<KeyConverter>();

    builder.Property(e => e.Password)
      .HasConversion<PasswordConverter>()
      .IsRequired(false);

    builder.Property(e => e.Email)
      .HasConversion<EmailConverter>();

    builder.OwnsMany(e => e.Claims, sp =>
    {
      sp.ToTable("UserClaims");

      sp.WithOwner().HasForeignKey("UserId");
    });
  }
}
