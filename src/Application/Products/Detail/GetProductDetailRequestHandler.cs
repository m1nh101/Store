using Application.Contracts;
using Application.Products.Specifications;
using Domain.Specifications;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Detail;

public sealed class GetProductDetailRequestHandler : IRequestHandler<GetProductDetailRequest, HandleResponse>
{
  private readonly IStoreContext _context;

  public GetProductDetailRequestHandler(IStoreContext context)
  {
    _context = context;
  }

  public async Task<HandleResponse> Handle(GetProductDetailRequest request, CancellationToken cancellationToken)
  {
    var specficiation = new GetProductById(request.Id);

    var product = await _context.Products.AsNoTracking()
      .FirstOrDefaultAsync(specficiation.ToExpression(), cancellationToken);

    if (product is null)
      throw new NullReferenceException(nameof(product));

    var data = new
    {
      product.Name,
      product.Brand,
      Id = product.Id.ToString(),
      Images = product.Images.Select(e => e.Url),
      Items = product.Items.Select(e => new
      {
        Id = e.Id.ToString(),
        e.Color,
        e.Size,
        e.Quantity,
        e.AdditionPrice
      }),
    };

    return HandleResponse.Success(data);
  }
}