using Application.Contracts;
using Domain.Events;
using MediatR;

namespace Application.Orders.EventHandlers;

public class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
{
    private readonly IBasketRepository _baskets;

    public OrderCreatedEventHandler(IBasketRepository baskets)
    {
        _baskets = baskets;
    }

    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        _baskets.RemoveBasket(notification.Order.UserId.ToString());

        return Task.CompletedTask;
    }
}
