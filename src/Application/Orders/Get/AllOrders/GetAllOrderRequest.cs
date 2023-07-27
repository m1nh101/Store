using Application.Contracts;
using Domain.Enums;
using MediatR;

namespace Application.Orders.Get.AllOrders;

public sealed record GetAllOrderRequest : IRequest<HandleResponse>
{
  public OrderState? State { get; init; }
  public DateTime? Date { get; init; }
}
