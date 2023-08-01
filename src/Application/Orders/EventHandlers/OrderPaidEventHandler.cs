using Application.Contracts;
using Application.Orders.Specifications;
using Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.EventHandlers;

public sealed class OrderPaidEventHandler : INotificationHandler<OrderPaidEvent>
{
  private readonly IStoreContext _context;

  public OrderPaidEventHandler(IStoreContext context)
  {
    _context = context;
  }

  public async Task Handle(OrderPaidEvent notification, CancellationToken cancellationToken)
  {
    var specification = new GetProductInOrderSpecification(notification.Order.Items);

    var products = await _context.Products.Where(specification.ToExpression()).ToListAsync(cancellationToken);

    //foreach(var product in products)
    //{
    //  var item = notification.Order.Items.FirstOrDefault(e => e.ProductId.Equals(product.Id));

    //  if (item == null)
    //    throw new NullReferenceException(nameof(item));

    //  var stock = product.Stock - item.Quantity;

    //  product.UpdateStock(stock);
    //}

    await _context.Commit(cancellationToken);
  }
}
