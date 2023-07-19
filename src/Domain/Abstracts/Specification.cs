using System.Linq.Expressions;

namespace Domain.Abstracts;

public abstract class Specification<T>
{
  public abstract Expression<Func<T, bool>> ToExpression();
  
  public bool IsSastifiedBy(T entity) => ToExpression().Compile().Invoke(entity);
}
