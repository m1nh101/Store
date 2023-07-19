using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Database.Converters;

internal class KeyConverter : ValueConverter<Identitifer, string>
{
  public KeyConverter()
    : base(e => e.Id, v => new Identitifer { Id = v })
  {

  }
}
