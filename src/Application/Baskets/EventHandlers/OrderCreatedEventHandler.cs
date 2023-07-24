using Application.Common;
using Application.Contracts;
using Domain.Events;
using MediatR;

namespace Application.Baskets.EventHandlers;

public class OrderCreatedEventHandler : INotificationHandler<EventNotification<OrderCreatedEvent>>
{
  private readonly IBasketRepository _baskets;

  public OrderCreatedEventHandler(IBasketRepository baskets)
  {
    _baskets = baskets;
  }

  public Task Handle(EventNotification<OrderCreatedEvent> notification, CancellationToken cancellationToken)
  {
    _baskets.RemoveBasket(notification.Event.Order.UserId.ToString());

    return Task.CompletedTask;
  }
}
