using Application.Baskets.AddItem;
using Application.Baskets.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public static class BasketEndpoint
{
  private const string GET_BASKET = "/api/baskets";
  private const string ADD_ITEM = "/api/baskets/items";

  public static WebApplication SetupBasketEndpoint(this WebApplication app)
  {
    app.MapGet(GET_BASKET, async ([FromServices] IMediator mediator) =>
    {
      var request = new GetBasketRequest();

      var response = await mediator.Send(request);

      return Results.Ok(response.Data);
    }).RequireAuthorization("SignedInUser");

    app.MapPost(ADD_ITEM, async ([FromServices] IMediator mediator,
      [FromBody] AddItemRequest request) =>
    {
      var response = await mediator.Send(request);

      if (response.Data == null)
        return Results.BadRequest(response.Error);

      return Results.Ok(response.Data);
    });

    return app;
  }
}
