namespace Domain.ValueObjects;

public sealed record Identifier
{
  public required string Id { get; init; }

  public static Identifier Init(string? id = "")
  {
    if(string.IsNullOrEmpty(id))
      return new() { Id = Guid.NewGuid().ToString().Replace("-", "") };

    return new() { Id = id };

  }
  public override string ToString() => Id;
}
