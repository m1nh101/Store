using Application.Contracts;
using Domain.Enums;
using MediatR;

namespace Application.Orders.Get.UserOrders;

public sealed record GetUserOrderRequest : IRequest<HandleResponse>
{
  public OrderState? State { get; init; }
}
