using Domain.Enums;

namespace Application.Orders.Get.Responses;

public sealed record OrderResponse
{
  public required int Id { get; init; }
  public required string Name { get; init; }
  public required int Count { get; init; }
  public required OrderState State { get; init; }
}
