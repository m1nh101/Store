using Application.Contracts;
using MediatR;

namespace Application.Products.Detail;

public sealed record GetProductDetailRequest : IRequest<HandleResponse>
{
  public required string Id { get; init; }
}
