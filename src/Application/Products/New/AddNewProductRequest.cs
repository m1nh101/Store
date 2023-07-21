using Application.Contracts;
using MediatR;

namespace Application.Products.New;

public sealed record AddNewProductRequest : IRequest<HandleResponse>
{
  public required string Name { get; init; }
  public required string Brand { get; init; }
  public required double Price { get; init; }
  public required int Stock { get; init; }
  public string[]? Images { get; set; }
}
