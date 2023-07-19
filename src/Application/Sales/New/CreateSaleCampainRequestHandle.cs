using Application.Contracts;
using Domain.Entities.Products;
using Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Sales.New;

public sealed class CreateSaleCampainRequestHandle
  : IRequestHandler<CreateSaleCampainRequest, HandleResponse>
{
  private readonly IStoreContext _context;

  public CreateSaleCampainRequestHandle(IStoreContext context)
  {
    _context = context;
  }

  public async Task<HandleResponse> Handle(CreateSaleCampainRequest request, CancellationToken cancellationToken)
  {
    var sale = new Sale(request.Name, request.StartDate, request.EndDate, request.Value);

    if(request.Products is not null)
    {
      var productIds = request.Products.Select(e => Identitifer.Init(e));

      var products = await _context.Products
        .Where(d => productIds.Any(e => e.Equals(d.Id)))
        .ToArrayAsync(cancellationToken);

      sale.ApplyToProduct(products);
    }

    await _context.Sales.AddAsync(sale, cancellationToken);

    await _context.Commit(cancellationToken);

    return HandleResponse.Success(new { Id = sale.Id.ToString() });
  }
}