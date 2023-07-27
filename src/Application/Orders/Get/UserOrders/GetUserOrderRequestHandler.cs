using Application.Contracts;
using Application.Orders.Specifications;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Get.UserOrders;

public sealed class GetUserOrderRequestHandler : IRequestHandler<GetUserOrderRequest, HandleResponse>
{
  private readonly IUserContext _userContext;
  private readonly IStoreContext _context;

  public GetUserOrderRequestHandler(IUserContext userContext, IStoreContext storeContext)
  {
    _userContext = userContext;
    _context = storeContext;
  }

  public async Task<HandleResponse> Handle(GetUserOrderRequest request, CancellationToken cancellationToken)
  {
    var specification = new GetOrderByUserIdSpecification(_userContext.Id, request.State);

    var orders = await _context.Orders.AsNoTracking()
      .Where(specification.ToExpression())
      .Select(new SelectOrderSpecification().ToSelector())
      .ToListAsync(cancellationToken);

    return HandleResponse.Success(orders);
  }
}
