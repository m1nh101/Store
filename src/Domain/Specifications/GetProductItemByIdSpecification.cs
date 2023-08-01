using Domain.Entities.Products;

namespace Domain.Specifications;

public class GetProductItemByIdSpecification : GetEntityById<ProductItem>
{
  public GetProductItemByIdSpecification(string id)
    :base(id)
  {
  }
}
