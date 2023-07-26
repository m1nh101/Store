using Domain.Abstracts;
using Domain.Entities.Orders;
using System.Linq.Expressions;

namespace Application.Orders.Specifications;

public class GetOrderbyIdSpecification : Specification<Order>
{
  private readonly int _id;

  public GetOrderbyIdSpecification(int id)
  {
    _id = id;
  }

  public override Expression<Func<Order, bool>> ToExpression() => e => e.Id == _id;
}
