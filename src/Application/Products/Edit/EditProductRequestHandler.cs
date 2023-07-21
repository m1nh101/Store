using Application.Contracts;
using Application.Products.Specifications;
using Domain.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Edit;

public sealed class EditProductRequestHandler : IRequestHandler<EditProductRequest, HandleResponse>
{
  private readonly IStoreContext _context;

  public EditProductRequestHandler(IStoreContext context)
  {
    _context = context;
  }

  public async Task<HandleResponse> Handle(EditProductRequest request, CancellationToken cancellationToken)
  {
    var specification = new GetProductById(request.Id);

    var product = await _context.Products.FirstOrDefaultAsync(specification.ToExpression(), cancellationToken);

    if (product == null)
      return HandleResponse.Fail(new { message = "Product not found" });

    var payload = new Product(request.Name, request.Brand, request.Price);

    product.Update(payload);
    product.UpdateStock(request.Stock);

    await _context.Commit(cancellationToken);

    return HandleResponse.Success(new { message = "update success"});
  }
}