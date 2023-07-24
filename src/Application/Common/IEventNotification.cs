using Domain.Abstracts;
using MediatR;

namespace Application.Common;

public class EventNotification<TEvent>
 : INotification
 where TEvent : IDomainEvent
{
  public required TEvent Event { get; set; }
}
