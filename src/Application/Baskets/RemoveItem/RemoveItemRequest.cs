using Application.Contracts;
using MediatR;

namespace Application.Baskets.RemoveItem;

public sealed record RemoveItemRequest : IRequest<HandleResponse>
{
  public required string ProductId { get; set; }
}
