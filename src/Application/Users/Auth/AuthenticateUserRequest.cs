using Application.Contracts;
using Application.Users.Specifications;
using Application.Users.Token;
using Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Auth;

public sealed record AuthenticateUserRequest : IRequest<HandleResponse>
{
  public required string Username { get; set; }
  public required string Password { get; set; }
}

public sealed class AuthenticateUserRequestHandler
  : IRequestHandler<AuthenticateUserRequest, HandleResponse>
{
  private readonly IStoreContext _context;
  private readonly JwtGenerator _tokenProvder;

  public AuthenticateUserRequestHandler(IStoreContext context,
    JwtGenerator tokenProvder)
  {
    _context = context;
    _tokenProvder = tokenProvder;
  }

  public async Task<HandleResponse> Handle(AuthenticateUserRequest request, CancellationToken cancellationToken)
  {
    // same params due to request.username can be username or email
    var specification = new GetUserSpecification(request.Username, request.Username);

    var user = await _context.Users
      .AsNoTracking()
      .FirstOrDefaultAsync(specification.ToExpression(), cancellationToken);

    var isFailedAuthentication = user is null || !user.Password.Equals(Password.Init(request.Password));

    if (isFailedAuthentication)
      return HandleResponse.Fail(new { message = "wrong credential" });

    var token = _tokenProvder.GenerateToken(user!);

    return HandleResponse.Success(token);
  }
}