using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Database.Converters;

internal class KeyConverter : ValueConverter<Identifier, string>
{
  public KeyConverter()
    : base(e => e.Id, v => new Identifier { Id = v })
  {

  }
}
