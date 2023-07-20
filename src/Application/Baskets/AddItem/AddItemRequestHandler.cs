using Application.Contracts;
using Application.Products.Specifications;
using Domain.Entities.Baskets;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Baskets.AddItem;

public sealed class AddItemRequestHandler : IRequestHandler<AddItemRequest, HandleResponse>
{
  private readonly IBasketRepository _baskets;
  private readonly IStoreContext _context;
  private readonly IUserContext _userContext;

  public AddItemRequestHandler(IBasketRepository baskets,
    IStoreContext context,
    IUserContext userContext)
  {
    _baskets = baskets;
    _context = context;
    _userContext = userContext;
  }

  public async Task<HandleResponse> Handle(AddItemRequest request, CancellationToken cancellationToken)
  {
    var productSpecification = new GetProductById(request.ProductId);

    var product = await _context.Products.FirstOrDefaultAsync(productSpecification.ToExpression(), cancellationToken);

    if (product is null)
      return HandleResponse.Fail(new { Message = "product not found" });

    if (product.Stock < request.Quantity)
      throw new ArgumentOutOfRangeException(nameof(request.Quantity));

    var basket = await _baskets.Get(_userContext.Id);

    var total = basket.AddItem(new BasketItem
    {
      ProductId = product.Id.ToString(),
      Quantity = request.Quantity,
      Price = product.Price,
      Name = product.Name,
      Image = product.Images.First().ToString(),
    });

    await _baskets.Save(basket);

    return HandleResponse.Success(new { Message = "Ok", Total =  total });
  }
}