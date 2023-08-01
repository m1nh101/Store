using API.Configurations;
using Application.Users.Auth;
using Application.Users.New;
using Application.Users.Token;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public static class IdentityEndpoint
{
  private const string TagName = "Identity";
  private const string SignIn = "/api/identity/auth";
  private const string SignUp = "/api/identity/register";
  private const string SignOut = "/api/identity/logout";
  private const string AccessToken = "access_token";

  public static WebApplication SetupIdentityEndpoint(this WebApplication app)
  {
    app.MapPost(SignIn, async ([FromServices] IMediator mediator,
      HttpContext http,
      [FromBody] AuthenticateUserRequest request) =>
    {
      var response = await mediator.Send(request);

      if (response.Data != null)
      {
        http.Response.Cookies.Append(AccessToken, (response.Data as GenerateTokenResult)!.Token);

        return Results.Ok(new { message = "login success"});
      }

      return Results.BadRequest(response.Error);

    }).WithOpenApi()
      .WithName("Sign In")
      .WithTags(TagName);

    app.MapPost(SignUp, async ([FromServices] IMediator mediator,
      HttpContext http,
      [FromBody] CreateNewUserRequest request) =>
    {
      var response = await mediator.Send(request);

      if(response.Data != null)
      {
        http.Response.Cookies.Append(AccessToken, (response.Data as GenerateTokenResult)!.Token);

        return Results.Ok(new { message = "register success"});
      }

      return Results.BadRequest(response.Error);
    }).WithOpenApi()
      .WithName("register new user")
      .WithTags(TagName);

    app.MapDelete(SignOut, (HttpContext http) =>
    {
      http.Response.Cookies.Delete(AccessToken);

      return Results.NoContent();
    }).WithOpenApi()
      .WithName("Sign out")
      .WithDescription("remove access_token")
      .WithTags(TagName)
      .RequireAuthorization(AuthorizePolicy.SignedIn);

    return app;
  }
}