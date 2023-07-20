using Application.Contracts;
using MediatR;

namespace Application.Baskets.AddItem;

public sealed record AddItemRequest : IRequest<HandleResponse>
{
  public required string ProductId { get; init; }
  public required int Quantity { get; init; }
}
