using Domain.Abstracts;
using Domain.Entities.Orders;
using Domain.Enums;
using Domain.ValueObjects;
using System.Linq.Expressions;

namespace Application.Orders.Specifications;

public sealed class GetOrderByUserIdSpecification : Specification<Order>
{
  private readonly Identifier _id;
  private readonly OrderState? _state;

  public GetOrderByUserIdSpecification(string id, OrderState? state)
  {
    _id = Identifier.Init(id);
    _state = state;
  }

  public override Expression<Func<Order, bool>> ToExpression()
  {
    return e => e.UserId.Equals(_id) && (!_state.HasValue || _state.Value == e.Status);
  }
}
