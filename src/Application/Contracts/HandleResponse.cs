namespace Application.Contracts;

public class HandleResponse
{
  public object? Data { get; private set; }
  public object? Error { get; private set; }

  public static HandleResponse Success(object data) => new() { Data = data };
  public static HandleResponse Fail(object data) => new() { Error = data };
}
