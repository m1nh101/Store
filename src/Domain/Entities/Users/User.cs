using Domain.Abstracts;
using Domain.ValueObjects;

namespace Domain.Entities.Users;

public class User : Entity
{
  private User() { }

  public User(string fullName, string email, string rawPassword, string? id = "", string username = "")
  {
    Id = Identifier.Init(id);
    FullName = fullName;
    Email = new Email(email);
    Password = Password.Init(rawPassword);
    UserName = username;
  }

  public string FullName { get; private set; } = string.Empty;
  public Email Email { get; private set; } = null!;
  public string UserName { get; private set; } = string.Empty;
  public Password Password { get; private set; } = null!;

  private readonly List<UserClaim> _claims = new();
  public IReadOnlyCollection<UserClaim> Claims => _claims.AsReadOnly();

  public void AddClaims(params UserClaim[] claims)
  {
    foreach (var claim in claims)
    {
      if (_claims.Any(e => e.Equals(claim)))
        continue;

      _claims.Add(claim);
    }
  }
}
