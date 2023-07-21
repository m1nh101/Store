using Newtonsoft.Json;

namespace API.Middlewares;

public class ErrorHandlingMiddleware
{
  private readonly RequestDelegate _next;

  public ErrorHandlingMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await _next(context);
    } catch (Exception ex)
    {
      context.Response.StatusCode = 400;
      context.Response.ContentType = "application/json";
      await context.Response.WriteAsync(JsonConvert.SerializeObject(new
      {
        StatusCode = 400,
        Error = ex.Message,
      }));
      
    }
  }
}
