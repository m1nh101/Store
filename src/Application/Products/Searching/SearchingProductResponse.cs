namespace Application.Products.Searching;

public sealed record SearchingProductResponse
{
  public required string Id { get; init; }
  public required string Name { get; init; }
  public required double Price { get; init; }
  public required string Image { get; init; }
}
