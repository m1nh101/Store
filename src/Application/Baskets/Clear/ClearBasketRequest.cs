using Application.Contracts;
using MediatR;

namespace Application.Baskets.Clear;

public sealed record ClearBasketRequest : IRequest<HandleResponse>;
