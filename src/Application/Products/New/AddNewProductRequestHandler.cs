using Application.Contracts;
using Domain.Entities.Products;
using MediatR;

namespace Application.Products.New;

public sealed class AddNewProductRequestHandler
  : IRequestHandler<AddNewProductRequest, HandleResponse>
{
  private readonly IStoreContext _context;

  public AddNewProductRequestHandler(IStoreContext context)
  {
    _context = context;
  }

  public async Task<HandleResponse> Handle(AddNewProductRequest request, CancellationToken cancellationToken)
  {
    var product = new Product(request.Name, request.Brand, request.Price);

    if(request.Images != null)
      product.AddImages(request.Images);

    if(request.Items != null)
    {
      var items = request.Items
        .Select(e => new ProductItem(e.Color, e.Size, e.Quantity, e.Price))
        .ToArray();

      product.AddItems(items);
    }

    await _context.Products.AddAsync(product, cancellationToken);

    await _context.Commit(cancellationToken);

    return HandleResponse.Success(new AddNewProductResponse { Id = product.Id.ToString() });
  }
}