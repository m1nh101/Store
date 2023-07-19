using Application.Contracts;
using Application.Users.Token;
using Domain.Entities.Users;
using MediatR;

namespace Application.Users.New;

public sealed class CreateNewUserRequestHandler :
  IRequestHandler<CreateNewUserRequest, HandleResponse>
{
  private readonly IStoreContext _context;
  private readonly JwtGenerator _tokenProvider;

  public CreateNewUserRequestHandler(IStoreContext context,
    JwtGenerator tokenProvider)
  {
    _context = context;
    _tokenProvider = tokenProvider;
  }

  public async Task<HandleResponse> Handle(CreateNewUserRequest request, CancellationToken cancellationToken)
  {
    // missing validation

    var user = new User(request.FullName, request.Email, request.Password, request.Id);

    if(request.Claims is not null)
      user.AddClaims(request.Claims);

    await _context.Users.AddAsync(user, cancellationToken);

    await _context.Commit(cancellationToken);

    //generate token

    var token = _tokenProvider.GenerateToken(user);

    return HandleResponse.Success(token);
  }
}