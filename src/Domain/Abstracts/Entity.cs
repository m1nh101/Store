using Domain.ValueObjects;

namespace Domain.Abstracts;

public abstract class Entity
{
  public Identifier Id { get; protected set; } = null!;
  public DateTime LastTimeModified { get; set; }
}
