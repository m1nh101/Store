using Application.Products.Searching;
using Domain.Abstracts;
using Domain.Entities.Products;
using System.Linq.Expressions;

namespace Application.Products.Specifications;

public sealed class SearchingProductSpecification : Specification<Product>
{
  private readonly SearchingProductRequest _request;

  public SearchingProductSpecification(SearchingProductRequest request)
  {
    _request = request;
  }

  public override Expression<Func<Product, bool>> ToExpression()
  {
    return e => (string.IsNullOrEmpty(_request.Name) || e.Name.StartsWith(_request.Name))
      && (string.IsNullOrEmpty(_request.Brand) || e.Brand.Equals(_request.Brand))
      && (!_request.PriceFrom.HasValue || e.Price >= _request.PriceFrom.Value)
      && (!_request.PriceTo.HasValue || e.Price <= _request.PriceTo.Value);
  }

  public Expression<Func<Product, SearchingProductResponse>> ToSelection()
  {
    return e => new SearchingProductResponse
    {
      Id = e.Id.ToString(),
      Name = e.Name,
      Price = e.Price,
      Image = e.Images.First().Url,
    };
  }
}
