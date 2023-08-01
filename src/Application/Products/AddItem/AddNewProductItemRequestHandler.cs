using Application.Contracts;
using Application.Products.Specifications;
using Domain.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.AddItem;

public sealed class AddNewProductItemRequestHandler
  : IRequestHandler<AddNewProductItemRequest, HandleResponse>
{
  private readonly IStoreContext _context;

  public AddNewProductItemRequestHandler(IStoreContext context)
  {
    _context = context;
  }

  public async Task<HandleResponse> Handle(AddNewProductItemRequest request, CancellationToken cancellationToken)
  {
    var specification = new GetProductById(request.ProductId);

    var product = await _context.Products.FirstOrDefaultAsync(specification.ToExpression(), cancellationToken);

    if(product is null)
      throw new NullReferenceException(nameof(product));

    var item = new ProductItem(request.Color, request.Size, request.Quantity, request.AdditionPrice);

    product.AddItems(item);

    await _context.Commit(cancellationToken);

    return HandleResponse.Success(new { Message = "Ok" });
  }
}