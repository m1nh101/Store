namespace Application.Users.Token;

public record GenerateTokenResult
{
  public required string Token { get; init; }
}