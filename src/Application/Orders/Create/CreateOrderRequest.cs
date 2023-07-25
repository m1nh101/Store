using Application.Contracts;
using MediatR;

namespace Application.Orders.Create;

public sealed record CreateOrderRequest : IRequest<HandleResponse>;
