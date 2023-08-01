using Application.Contracts;
using Domain.Entities.Products;
using MediatR;

namespace Application.Products.New;

public sealed record AddNewProductRequest : IRequest<HandleResponse>
{
  public required string Name { get; init; }
  public required string Brand { get; init; }
  public required double Price { get; init; }
  public string[]? Images { get; set; }

  public ProductItemPayload[]? Items { get; init; }
}

public sealed record ProductItemPayload
{
  public required string Color { get; init; }
  public required string Size { get; init; }
  public required int Quantity { get; init; }
  public required double Price { get; init; }
}