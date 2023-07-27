using Application.Orders.Get.AllOrders;
using Domain.Abstracts;
using Domain.Entities.Orders;
using System.Linq.Expressions;

namespace Application.Orders.Specifications;

public sealed class FilterOrderSpecification : Specification<Order>
{
  private readonly GetAllOrderRequest _request;

  public FilterOrderSpecification(GetAllOrderRequest request)
  {
    _request = request;
  }

  public override Expression<Func<Order, bool>> ToExpression()
  {
    return e => (!_request.State.HasValue || _request.State.Value == e.Status)
      && (!_request.Date.HasValue || _request.Date.Value == e.LastTimeModified);
  }
}
