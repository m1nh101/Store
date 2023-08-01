using Application.Contracts;
using Application.Products.Specifications;
using Domain.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.UpdateItem;

public sealed record UpdateProductItemRequestHandler : IRequestHandler<UpdateProductItemRequest, HandleResponse>
{
  private readonly IStoreContext _context;

  public UpdateProductItemRequestHandler(IStoreContext context)
  {
    _context = context;
  }

  public async Task<HandleResponse> Handle(UpdateProductItemRequest request, CancellationToken cancellationToken)
  {
    var specification = new GetProductById(request.ProductId);

    var product = await _context.Products.FirstOrDefaultAsync(specification.ToExpression(), cancellationToken);

    if (product is null)
      throw new NullReferenceException(nameof(product));

    var item = new ProductItem(request.ItemId, request.Quantity, request.AdditionPrice);

    product.AddItems(item);

    await _context.Commit(cancellationToken);

    return HandleResponse.Success(new { Message = "Ok" });
  }
}