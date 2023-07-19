using Application.Contracts;
using MediatR;

namespace Application.Sales.New;

public sealed record CreateSaleCampainRequest : IRequest<HandleResponse>
{
  public required string Name { get; init; }
  public required DateTime StartDate { get; init; }
  public required DateTime EndDate { get; init; }
  public required short Value { get; init; }
  public string[]? Products { get; init; }
}
