using Application.Contracts;
using MediatR;

namespace Application.Baskets.RemoveItem;

public sealed class RemoveItemRequestHandler : IRequestHandler<RemoveItemRequest, HandleResponse>
{
  private readonly IUserContext _userContext;
  private readonly IBasketRepository _baskets;

  public RemoveItemRequestHandler(IUserContext userContext,
    IBasketRepository baskets)
  {
    _userContext = userContext;
    _baskets = baskets;
  }

  public async Task<HandleResponse> Handle(RemoveItemRequest request, CancellationToken cancellationToken)
  {
    var basket = await _baskets.Get(_userContext.Id);

    basket.RemoveItem(request.ProductId);

    await _baskets.Save(basket);

    return HandleResponse.Success(new { basket.Total });
  }
}
