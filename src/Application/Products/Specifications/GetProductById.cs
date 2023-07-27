using Domain.Abstracts;
using Domain.Entities.Products;
using Domain.ValueObjects;
using System.Linq.Expressions;

namespace Application.Products.Specifications;

public sealed class GetProductById : Specification<Product>
{
  private readonly Identifier _id;

  public GetProductById(string id)
  {
    _id = Identifier.Init(id);
  }

  public override Expression<Func<Product, bool>> ToExpression()
  {
    return e => e.Id.Equals(_id);
  }
}
