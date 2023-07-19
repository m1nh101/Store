using System.Security.Cryptography;
using System.Text;

namespace Domain.ValueObjects;

public sealed record Password
{
  public string HashPassword { get; private set; } = null!;

  public static Password Init(string raw)
  {
    return new()
    {
      HashPassword = HashString(raw)
    };
  }

  public static Password Hash(string hashPassword)
  {
    return new()
    {
      HashPassword = hashPassword
    };
  }

  private static string HashString(string raw)
  {
    var bytes = Encoding.UTF8.GetBytes(raw);
    var hashBytes = MD5.HashData(bytes);

    return Convert.ToHexString(hashBytes);
  }
}
