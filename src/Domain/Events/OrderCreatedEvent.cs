using Domain.Abstracts;
using Domain.Entities.Orders;

namespace Domain.Events;

public sealed record OrderCreatedEvent
(Order Order) : IDomainEvent;
