using Application.Baskets.RemoveItem;
using Application.Contracts;
using MediatR;

namespace Application.Products.RemoveItem;

public sealed record RemoveProductItemRequest : IRequest<HandleResponse>
{
  public required string ProductId { get; init; }
  public required string ItemId { get; init; } 
}