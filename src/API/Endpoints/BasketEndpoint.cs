﻿using Application.Baskets.AddItem;
using Application.Baskets.Clear;
using Application.Baskets.Get;
using Application.Baskets.RemoveItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public static class BasketEndpoint
{
  private const string TAG_NAME = "Basket";
  private const string GET_BASKET = "/api/baskets";
  private const string ADD_ITEM = "/api/baskets/items";
  private const string REMOVE_ITEM = "/api/baskets/items/{id}";
  private const string CLEAR_BASKET = "/api/baskets";

  public static WebApplication SetupBasketEndpoint(this WebApplication app)
  {
    app.MapGet(GET_BASKET, async ([FromServices] IMediator mediator) =>
    {
      var request = new GetBasketRequest();

      var response = await mediator.Send(request);

      return Results.Ok(response.Data);
    }).RequireAuthorization("SignedInUser")
      .WithOpenApi()
      .WithTags(TAG_NAME);

    app.MapPost(ADD_ITEM, async ([FromServices] IMediator mediator,
      [FromBody] AddItemRequest request) =>
    {
      var response = await mediator.Send(request);

      if (response.Data == null)
        return Results.BadRequest(response.Error);

      return Results.Ok(response.Data);
    }).WithOpenApi()
      .WithTags(TAG_NAME);

    app.MapDelete(REMOVE_ITEM, async ([FromServices] IMediator mediator,
      [FromRoute] string id) =>
    {
      var request = new RemoveItemRequest() { ProductId = id };

      var response = await mediator.Send(request);

      if (response.Error == null)
        return Results.Ok(response.Data);

      return Results.BadRequest(response.Error);
    }).WithOpenApi()
      .WithTags(TAG_NAME);

    app.MapDelete(CLEAR_BASKET, async ([FromServices] IMediator mediator) =>
    {
      var request = new ClearBasketRequest();

      await mediator.Send(request);

      return Results.NoContent();
    }).WithOpenApi()
      .WithTags(TAG_NAME);

    return app;
  }
}
