using API.Configurations;
using Application.Baskets.AddItem;
using Application.Baskets.Clear;
using Application.Baskets.Get;
using Application.Baskets.RemoveItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public static class BasketEndpoint
{
  private const string TagName = "Basket";
  private const string GetBasket = "/api/baskets";
  private const string AddItem = "/api/baskets/items";
  private const string RemoveItem = "/api/baskets/items/{id}";
  private const string ClearBasket = "/api/baskets";

  public static WebApplication SetupBasketEndpoint(this WebApplication app)
  {
    app.MapGet(GetBasket, async ([FromServices] IMediator mediator) =>
    {
      var request = new GetBasketRequest();

      var response = await mediator.Send(request);

      return Results.Ok(response.Data);
    }).RequireAuthorization(AuthorizePolicy.SignedIn)
      .WithOpenApi()
      .WithTags(TagName);

    app.MapPost(AddItem, async ([FromServices] IMediator mediator,
      [FromBody] AddItemRequest request) =>
    {
      var response = await mediator.Send(request);

      if (response.Data == null)
        return Results.BadRequest(response.Error);

      return Results.Ok(response.Data);
    }).WithOpenApi()
      .WithTags(TagName)
      .RequireAuthorization(AuthorizePolicy.SignedIn);

    app.MapDelete(RemoveItem, async ([FromServices] IMediator mediator,
      [FromRoute] string id) =>
    {
      var request = new RemoveItemRequest() { ItemId = id };

      var response = await mediator.Send(request);

      if (response.Error == null)
        return Results.Ok(response.Data);

      return Results.BadRequest(response.Error);
    }).WithOpenApi()
      .WithTags(TagName)
      .RequireAuthorization(AuthorizePolicy.SignedIn);

    app.MapDelete(ClearBasket, async ([FromServices] IMediator mediator) =>
    {
      var request = new ClearBasketRequest();

      await mediator.Send(request);

      return Results.NoContent();
    }).WithOpenApi()
      .WithTags(TagName)
      .RequireAuthorization(AuthorizePolicy.SignedIn);

    return app;
  }
}
