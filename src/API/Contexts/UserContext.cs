using Application.Contracts;
using System.Security.Claims;

namespace API.Contexts;

public sealed class UserContext : IUserContext
{
  private readonly IHttpContextAccessor _http;

  public UserContext(IHttpContextAccessor http)
  {
    _http = http;
  }

  public string Id => _http.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
    ?? throw new NullReferenceException("how unauthenticate user can go to this");

  public string Email => _http.HttpContext?.User.FindFirstValue(ClaimTypes.Email)
    ?? throw new NullReferenceException("how unauthenticate user can go to this");

  public bool IsSuperUser => _http.HttpContext?.User.FindFirstValue(ClaimTypes.Role)?.Equals("admin")
    ?? false;
}
