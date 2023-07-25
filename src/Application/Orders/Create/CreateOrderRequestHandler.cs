using Application.Contracts;
using Domain.Entities.Orders;
using MediatR;

namespace Application.Orders.Create;

public sealed class CreateOrderRequestHandler : IRequestHandler<CreateOrderRequest, HandleResponse>
{
  private readonly IBasketRepository _baskets;
  private readonly IUserContext _userContext;
  private readonly IStoreContext _context;

  public CreateOrderRequestHandler(IBasketRepository baskets,
    IUserContext userContext,
    IStoreContext context)
  {
    _baskets = baskets;
    _userContext = userContext;
    _context = context;
  }

  public async Task<HandleResponse> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
  {
    var basket = await _baskets.Get(_userContext.Id);

    if(basket == null || basket.Items.Count == 0)
      throw new NullReferenceException(nameof(basket));

    var items = basket.Items.Select(e => new OrderItem
    {
      Name = e.Name,
      Price = e.Price,
      Quantity = e.Quantity,
    }).ToList();

    var order = Order.Create(_userContext.Id, items);

    await _context.Orders.AddAsync(order, cancellationToken);

    await _context.Commit(cancellationToken);

    return HandleResponse.Success(new { Ok = "Ok" });
  }
}