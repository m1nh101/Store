namespace Application.Common;

public record ValidationItem
{
  public required string Field { get; init; }
  public required string Message { get; init; }
}

public class ValidationResult
{
  public IEnumerable<ValidationItem>? Errors { get; init; }

  public bool IsValid => Errors == null || !Errors.Any();
}
