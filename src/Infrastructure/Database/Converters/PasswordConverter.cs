using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Database.Converters;

internal class PasswordConverter : ValueConverter<Password, string>
{
  public PasswordConverter()
    :base(e => e.HashPassword, d => Password.Hash(d))
  {
  }
}