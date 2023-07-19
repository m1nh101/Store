using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Database.Converters;

internal class EmailConverter : ValueConverter<Email, string>
{
  public EmailConverter()
    :base(e => e.ToString(), d => new Email(d))
  {
  }
}