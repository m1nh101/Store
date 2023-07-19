namespace Domain.Entities.Users;

public sealed record UserClaim
{
  public required string ClaimType { get; init; }
  public required string Value { get; init; }
}