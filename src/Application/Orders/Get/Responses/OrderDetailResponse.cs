using Domain.Entities.Orders;
using Domain.Enums;

namespace Application.Orders.Get.Responses;

public sealed record OrderDetailResponse
{
  public required int Id { get; init; }
  public required string Name { get; init; }
  public required OrderState State { get; init; }
  public required double Total { get; init; } 
  public required IEnumerable<OrderItem> Items { get; init; }
}