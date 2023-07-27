using Application.Orders.Get.Responses;
using Domain.Entities.Orders;
using System.Linq.Expressions;

namespace Application.Orders.Specifications;

public sealed class SelectOrderSpecification
{
  public Expression<Func<Order, OrderResponse>> ToSelector()
  {
    return e => new OrderResponse
    {
      Id = e.Id,
      Name = "Đơn hàng " + e.Id + e.LastTimeModified.ToString("dd/MM/yyyy"),
      Count = e.Items.Count(),
      State = e.Status
    };
  }
}
