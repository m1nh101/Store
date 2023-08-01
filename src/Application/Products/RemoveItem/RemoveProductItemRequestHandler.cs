using Application.Contracts;
using Application.Products.Specifications;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.RemoveItem;

public sealed class RemoveProductItemRequestHandler : IRequestHandler<RemoveProductItemRequest, HandleResponse>
{
  private readonly IStoreContext _context;

  public RemoveProductItemRequestHandler(IStoreContext context)
  {
    _context = context;
  }

  public async Task<HandleResponse> Handle(RemoveProductItemRequest request, CancellationToken cancellationToken)
  {
    var specification = new GetProductById(request.ProductId);

    var product = await _context.Products.FirstOrDefaultAsync(specification.ToExpression(), cancellationToken);

    if (product is null)
      throw new NullReferenceException(nameof(product));

    product.RemoveItem(request.ItemId);

    await _context.Commit(cancellationToken);

    return HandleResponse.Success(new { Message = "Ok" });
  }
}