using Domain.Abstracts;
using Domain.Entities.Products;
using Domain.ValueObjects;
using System.Linq.Expressions;

namespace Application.Products.Specifications;

public sealed class GetProductById : Specification<Product>
{
  private readonly Identitifer _id;

  public GetProductById(string id)
  {
    _id = Identitifer.Init(id);
  }

  public override Expression<Func<Product, bool>> ToExpression()
  {
    return e => e.Id.Equals(_id);
  }
}
