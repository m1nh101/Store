using Domain.Abstracts;
using Domain.Events;
using Domain.ValueObjects;

namespace Domain.Entities.Orders;

public class Order : AggregateRoot
{
  private Order() { }

  public int Id { get; set; }
  public Address Address { get; set; } = null!;
  public DateTime PaidTime { get; set; }
  public Identitifer UserId { get; set; } = null!;

  public IEnumerable<OrderItem> Items { get; set; } = null!;

  public static Order Create(string userId, Address address, IEnumerable<OrderItem> items)
  {
    var order = new Order
    {
      UserId = Identitifer.Init(userId),
      Address = address,
      Items = items
    };

    order.AddEvent(new OrderCreatedEvent(order));

    return order;
  }
}
