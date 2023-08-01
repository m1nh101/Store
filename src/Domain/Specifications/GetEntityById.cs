using Domain.Abstracts;
using Domain.ValueObjects;
using System.Linq.Expressions;

namespace Domain.Specifications;

public abstract class GetEntityById<TEntity> : Specification<TEntity>
  where TEntity : Entity
{
  private readonly Identifier _id;

  protected GetEntityById(string id)
  {
    _id = Identifier.Init(id);
  }

  public sealed override Expression<Func<TEntity, bool>> ToExpression()
  {
    return e => e.Id == _id;
  }
}