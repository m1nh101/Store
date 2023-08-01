using Application.Contracts;
using MediatR;

namespace Application.Products.UpdateItem;

public sealed record UpdateProductItemRequest : IRequest<HandleResponse>
{
  public required double AdditionPrice { get; init; } = 0;
  public required int Quantity { get; init; }
  public string ItemId { get; init; } = null!;
  public string ProductId { get; init; } = null!;
}
