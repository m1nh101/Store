using Domain.ValueObjects;

namespace Domain.Entities.Orders;

public record OrderItem
{
  public required string Name { get; init; }
  public required double Price { get; init; }
  public required int Quantity { get; init; }
  public required Identitifer ProductId { get; init; }
}
