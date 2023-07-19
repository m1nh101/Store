namespace Application.Users.Token;

public sealed class JwtOption
{
  public string SecretKey { get; set; } = string.Empty;
  public int ExpiredIn { get; set; }
}