using Application.Contracts;
using MediatR;

namespace Application.Baskets.Clear;

public sealed record ClearBasketRequestHandler : IRequestHandler<ClearBasketRequest, HandleResponse>
{
  private readonly IBasketRepository _baskets;
  private readonly IUserContext _userContext;

  public ClearBasketRequestHandler(IBasketRepository baskets,
    IUserContext userContext)
  {
    _baskets = baskets;
    _userContext = userContext;
  }

  public Task<HandleResponse> Handle(ClearBasketRequest request, CancellationToken cancellationToken)
  {
    _baskets.RemoveBasket(_userContext.Id);

    return Task.FromResult(HandleResponse.Success(new { Message = "Ok" }));
  }
}
