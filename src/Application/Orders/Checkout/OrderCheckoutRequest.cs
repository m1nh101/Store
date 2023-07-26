using Application.Contracts;
using Domain.Enums;
using MediatR;

namespace Application.Orders.Checkout;

public sealed record OrderCheckoutRequest : IRequest<HandleResponse>
{
  public required int Id { get; set; }
  public required OrderState State { get; init; }
}