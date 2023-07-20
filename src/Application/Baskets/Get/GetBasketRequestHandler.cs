using Application.Contracts;
using MediatR;

namespace Application.Baskets.Get;

public sealed class GetBasketRequestHandler : IRequestHandler<GetBasketRequest, HandleResponse>
{
  private readonly IBasketRepository _baskets;
  private readonly IUserContext _userContext;

  public GetBasketRequestHandler(IBasketRepository baskets,
    IUserContext userContext)
  {
    _baskets = baskets;
    _userContext = userContext;
  }

  public async Task<HandleResponse> Handle(GetBasketRequest request, CancellationToken cancellationToken)
  {
    var basket = await _baskets.Get(_userContext.Id);

    return HandleResponse.Success(basket);
  }
}