namespace Domain.ValueObjects;

public sealed record ProductImage
{
  public required string Url { get; init; }
}
