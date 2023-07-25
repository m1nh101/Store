using Domain.Abstracts;
using Domain.Enums;
using Domain.Events;
using Domain.ValueObjects;

namespace Domain.Entities.Orders;

public class Order : AggregateRoot
{
  private Order() { }

  public int Id { get; private set; }
  public Address Address { get; private set; } = null!;
  public DateTime PaidTime { get; private set; }
  public Identitifer UserId { get; private set; } = null!;
  public OrderState Status { get; private set; }

  public void ChangeStatus(OrderState state) => Status = state;

  public IEnumerable<OrderItem> Items { get; private set; } = null!;

  public static Order Create(string userId, IEnumerable<OrderItem> items)
  {
    var order = new Order
    {
      UserId = Identitifer.Init(userId),
      Address = new Address { Address1 = "temp"},
      Items = items
    };

    order.AddEvent(new OrderCreatedEvent(order));

    return order;
  }
}
