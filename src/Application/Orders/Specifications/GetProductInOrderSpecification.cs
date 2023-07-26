using Domain.Abstracts;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using System.Linq.Expressions;

namespace Application.Orders.Specifications;

public sealed class GetProductInOrderSpecification : Specification<Product>
{
  private readonly IEnumerable<OrderItem> _items;

  public GetProductInOrderSpecification(IEnumerable<OrderItem> items)
  {
    _items = items;
  }

  public override Expression<Func<Product, bool>> ToExpression()
  {
    return e => _items.Any(d => d.ProductId.Equals(e.Id));
  }
}
