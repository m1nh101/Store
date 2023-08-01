using Application.Contracts;
using MediatR;

namespace Application.Products.Edit;

public sealed record EditProductRequest : IRequest<HandleResponse>
{
  public required string Id { get; init; }
  public required string Name { get; init; }
  public required string Brand { get; init; }
  public required double Price { get; init; }
  public string[]? Images { get; init; }
  public string[]? RemoveImages { get; init; }
}