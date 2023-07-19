namespace Domain.Abstracts;

public abstract class AggregateRoot : Entity
{
  private readonly List<IDomainEvent> _events = new();

  public void AddEvent(IDomainEvent @event) => _events.Add(@event);
}
