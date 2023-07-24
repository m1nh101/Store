namespace Domain.ValueObjects;

public record Address
{
  public required string Address1 { get; init; }
  public string? Address2 { get; init; }
  public string? PostCode { get; init; }
}
