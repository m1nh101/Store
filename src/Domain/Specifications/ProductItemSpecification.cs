using Domain.Abstracts;
using Domain.Entities.Products;
using Domain.ValueObjects;
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

public class GetProductItemByIdSpecification : Specification<ProductItem>
{
  private readonly Identifier _id;

  public GetProductItemByIdSpecification(string id)
  {
    _id = Identifier.Init(id);
  }

  public override Expression<Func<ProductItem, bool>> ToExpression() => e => e.Id.Equals(_id);
}