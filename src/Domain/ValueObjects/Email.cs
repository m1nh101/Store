using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public sealed record Email
{
  private readonly string _email;
  private static readonly Regex MAIL_PATTERN = new(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

  public Email(string email)
  {
    if (MAIL_PATTERN.IsMatch(email))
      _email = email;
    else
      throw new ArgumentException(email + "not valid");
  }

  public override string ToString() => _email;
}
