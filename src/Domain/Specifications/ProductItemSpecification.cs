using Domain.Abstracts;
using Domain.Entities.Products;
using System.Linq.Expressions;

namespace Domain.Specifications;

public class ProductItemSpecification : Specification<ProductItem>
{
  private readonly ProductItem _item;

  public ProductItemSpecification(ProductItem item)
  {
    _item = item;
  }

  public override Expression<Func<ProductItem, bool>> ToExpression()
  {
    return e => e.Id.Equals(_item.Id)
      || (e.Size.ToLower() == _item.Size.ToLower() && e.Color.ToLower() == _item.Size.ToLower());
  }
}
