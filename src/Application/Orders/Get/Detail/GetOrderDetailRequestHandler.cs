using Application.Contracts;
using Application.Orders.Get.Responses;
using Application.Orders.Specifications;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Get.Detail;

public sealed class GetOrderDetailRequestHandler : IRequestHandler<GetOrderDetailRequest, HandleResponse>
{
  private readonly IUserContext _userContext;
  private readonly IStoreContext _context;

  public GetOrderDetailRequestHandler(IUserContext userContext, IStoreContext context)
  {
    _userContext = userContext;
    _context = context;
  }

  public async Task<HandleResponse> Handle(GetOrderDetailRequest request, CancellationToken cancellationToken)
  {
    var specification = new GetOrderDetailSpecification(request.Id, _userContext.IsSuperUser, _userContext.Id);

    var order = await _context.Orders.AsNoTracking()
      .FirstOrDefaultAsync(specification.ToExpression(), cancellationToken);

    if (order == null)
      throw new NullReferenceException(nameof(order));

    var data = new OrderDetailResponse
    {
      Id = order.Id,
      Name = $"Đơn hàng {order.Id}#{order.LastTimeModified:dd/MM/yyyy}",
      Total = order.Items.Sum(d => d.Quantity * d.Price),
      Items = order.Items,
      State = order.Status
    };

    return HandleResponse.Success(data);
  }
}