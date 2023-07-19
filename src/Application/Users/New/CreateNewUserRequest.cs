using Application.Contracts;
using Domain.Entities.Users;
using MediatR;

namespace Application.Users.New;

public sealed record CreateNewUserRequest : IRequest<HandleResponse>
{
  public required string FullName { get; init; }
  public required string Email { get; init; }
  public string? Id { get; init; }
  public required string Password { get; init; }
  public UserClaim[]? Claims { get; init; }
}
