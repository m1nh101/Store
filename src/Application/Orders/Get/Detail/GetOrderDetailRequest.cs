using Application.Contracts;
using MediatR;

namespace Application.Orders.Get.Detail;

public sealed record GetOrderDetailRequest : IRequest<HandleResponse>
{
  public required int Id { get; init; }
}
