using Domain.Abstracts;
using Domain.Entities.Orders;
using Domain.ValueObjects;
using System.Linq.Expressions;

namespace Application.Orders.Specifications;

public sealed class GetOrderDetailSpecification : Specification<Order>
{
  private readonly int _id;
  private readonly bool _isSuperUser;
  private readonly Identifier _userId;

  public GetOrderDetailSpecification(int id, bool isSuperUser, string userId)
  {
    _id = id;
    _isSuperUser = isSuperUser;
    _userId = Identifier.Init(userId);
  }

  public override Expression<Func<Order, bool>> ToExpression()
  {
    if (_isSuperUser)
      return e => e.Id == _id;

    return e => e.Id == _id && e.UserId.Equals(_userId);
  }
}
