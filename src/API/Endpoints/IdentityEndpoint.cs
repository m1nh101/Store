using Application.Users.Auth;
using Application.Users.New;
using Application.Users.Token;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public static class IdentityEndpoint
{
  private const string ENDPONT_TAG = "Identity";
  private const string SIGN_IN = "/api/identity/auth";
  private const string SIGN_UP = "/api/identity/register";
  private const string SIGN_OUT = "/api/identity/logout";

  public static WebApplication SetupIdentityEndpoint(this WebApplication app)
  {
    app.MapPost(SIGN_IN, async ([FromServices] IMediator mediator,
      HttpContext http,
      [FromBody] AuthenticateUserRequest request) =>
    {
      var response = await mediator.Send(request);

      if (response.Data != null)
      {
        http.Response.Cookies.Append("access_token", (response.Data as GenerateTokenResult)!.Token);

        return Results.Ok(new { message = "login success"});
      }

      return Results.BadRequest(response.Error);

    }).WithOpenApi()
      .WithName("Sign In")
      .WithTags(ENDPONT_TAG);

    app.MapPost(SIGN_UP, async ([FromServices] IMediator mediator,
      HttpContext http,
      [FromBody] CreateNewUserRequest request) =>
    {
      var response = await mediator.Send(request);

      if(response.Data != null)
      {
        http.Response.Cookies.Append("access_token", (response.Data as GenerateTokenResult)!.Token);

        return Results.Ok(new { message = "register success"});
      }

      return Results.BadRequest(response.Error);
    }).WithOpenApi()
      .WithName("register new user")
      .WithTags(ENDPONT_TAG);

    app.MapDelete(SIGN_OUT, (HttpContext http) =>
    {
      http.Response.Cookies.Delete("access_token");

      return Results.NoContent();
    }).WithOpenApi()
      .WithName("Sign out")
      .WithDescription("remove access_token")
      .WithTags(ENDPONT_TAG)
      .RequireAuthorization("SignedUser");

    return app;
  }
}