using Application.Contracts;
using Application.Products.Specifications;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Searching;

public sealed class SearchingProductRequestHandler
  : IRequestHandler<SearchingProductRequest, HandleResponse>
{
  private readonly IStoreContext _context;

  public SearchingProductRequestHandler(IStoreContext context)
  {
    _context = context;
  }

  public async Task<HandleResponse> Handle(SearchingProductRequest request, CancellationToken cancellationToken)
  {
    var specification = new SearchingProductSpecification(request);

    var products = await _context.Products
      .Where(specification.ToExpression())
      .AsNoTracking()
      .Select(specification.ToSelection())
      .ToListAsync(cancellationToken);

    return HandleResponse.Success(products);
  }
}