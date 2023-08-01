using Application.Contracts;
using Application.Users.Specifications;
using Application.Users.Token;
using Domain.Entities.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.New;

public sealed class CreateNewUserRequestHandler :
  IRequestHandler<CreateNewUserRequest, HandleResponse>
{
  private readonly IStoreContext _context;
  private readonly IUserContext _userContext;
  private readonly JwtGenerator _tokenProvider;

  public CreateNewUserRequestHandler(IStoreContext context,
    JwtGenerator tokenProvider,
    IUserContext userContext)
  {
    _context = context;
    _tokenProvider = tokenProvider;
    _userContext = userContext;
  }

  public async Task<HandleResponse> Handle(CreateNewUserRequest request, CancellationToken cancellationToken)
  {
    var specification = new GetUserSpecification(request.Email, request.Email);

    var isExistUser = await _context.Users.AnyAsync(specification.ToExpression(), cancellationToken);

    if (isExistUser)
      return HandleResponse.Fail(new { Message = "Username or email has been used" });

    var user = new User(request.FullName, request.Email, request.Password, request.Id);

    if (_userContext.IsSuperUser && request.Claims != null)
      user.AddClaims(request.Claims);
    else
      user.AddClaims(new UserClaim { ClaimType = "role", Value = "user" });

    await _context.Users.AddAsync(user, cancellationToken);

    await _context.Commit(cancellationToken);

    //generate token

    var token = _tokenProvider.GenerateToken(user);

    return HandleResponse.Success(token);
  }
}