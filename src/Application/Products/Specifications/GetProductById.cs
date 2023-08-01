using Domain.Entities.Products;
using Domain.Specifications;

namespace Application.Products.Specifications;

public sealed class GetProductById : GetEntityById<Product>
{
  public GetProductById(string id)
    :base(id)
  {
  }
}
