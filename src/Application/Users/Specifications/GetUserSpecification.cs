using Domain.Abstracts;
using Domain.Entities.Users;
using Domain.ValueObjects;
using System.Linq.Expressions;

namespace Application.Users.Specifications;

public sealed class GetUserSpecification : Specification<User>
{
  private readonly string _username;
  private readonly Email _email;

  public GetUserSpecification(string username, string email = "")
  {
    _username = username;
    _email = new Email(email);
  }

  public override Expression<Func<User, bool>> ToExpression()
  {
    return e => e.UserName.Equals(_username) || e.Email.Equals(_email);
  }
}
