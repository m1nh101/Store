using Application.Contracts;
using MediatR;

namespace Application.Products.Searching;

public sealed record SearchingProductRequest : IRequest<HandleResponse>
{
  public string? Name { get; set; }
  public string? Brand { get; set; }
  public double? PriceFrom { get; set; }
  public double? PriceTo { get; set; }
}
