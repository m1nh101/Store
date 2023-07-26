using Application.Contracts;
using Application.Orders.Specifications;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Checkout;

public sealed class OrderCheckoutRequestHandler : IRequestHandler<OrderCheckoutRequest, HandleResponse>
{
  private readonly IStoreContext _context;

  public OrderCheckoutRequestHandler(IStoreContext context)
  {
    _context = context;
  }

  public async Task<HandleResponse> Handle(OrderCheckoutRequest request, CancellationToken cancellationToken)
  {
    var specification = new GetOrderbyIdSpecification(request.Id);

    var order = await _context.Orders.FirstOrDefaultAsync(specification.ToExpression(), cancellationToken);

    if (order == null)
      throw new NullReferenceException(nameof(order));

    order.ChangeStatus(order.Status);

    await _context.Commit(cancellationToken);

    return HandleResponse.Success(new { Message = "Ok" });
  }
}