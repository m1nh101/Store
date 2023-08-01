using Application.Contracts;
using MediatR;

namespace Application.Products.AddItem;

public sealed record AddNewProductItemRequest : IRequest<HandleResponse>
{
  public string ProductId { get; init; } = null!;
  public required string Color { get; init; }
  public required string Size { get; init; }
  public required int Quantity { get; init; }
  public required double AdditionPrice { get; init; } = default;
}