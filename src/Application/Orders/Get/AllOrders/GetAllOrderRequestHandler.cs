using Application.Contracts;
using Application.Orders.Specifications;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Get.AllOrders;

public sealed class GetAllOrderRequestHandler : IRequestHandler<GetAllOrderRequest, HandleResponse>
{
  private readonly IStoreContext _context;

  public GetAllOrderRequestHandler(IStoreContext context)
  {
    _context = context;
  }

  public async Task<HandleResponse> Handle(GetAllOrderRequest request, CancellationToken cancellationToken)
  {
    var specification = new FilterOrderSpecification(request);

    var orders = await _context.Orders.AsNoTracking()
      .Where(specification.ToExpression())
      .Select(new SelectOrderSpecification().ToSelector())
      .ToListAsync(cancellationToken);

    return HandleResponse.Success(orders);
  }
}
