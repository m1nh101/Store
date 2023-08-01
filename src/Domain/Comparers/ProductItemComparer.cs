using Domain.Entities.Products;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Comparers;

public sealed class ProductItemComparer : IEqualityComparer<ProductItem>
{
  public bool Equals(ProductItem? x, ProductItem? y)
  {
    if (x == null || y == null)
      return false;

    return x.Color.ToLower() == y.Color.ToLower() && x.Size.ToLower() == y.Size.ToLower() && y.Id.Equals(x.Id);
  }

  public int GetHashCode([DisallowNull] ProductItem obj)
  {
    return obj.Color.GetHashCode() ^ obj.Size.GetHashCode() ^ obj.Id.GetHashCode();
  }
}
