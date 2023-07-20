using Application.Contracts;
using MediatR;

namespace Application.Baskets.Get;

public record GetBasketRequest : IRequest<HandleResponse>
{
}
