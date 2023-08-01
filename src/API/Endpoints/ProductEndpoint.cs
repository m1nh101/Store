using Application.Products.AddItem;
using Application.Products.Detail;
using Application.Products.Edit;
using Application.Products.New;
using Application.Products.RemoveItem;
using Application.Products.Searching;
using Application.Products.UpdateItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public static class ProductEndpoint
{
  private const string TAG_NAME = "Product";
  private const string GET_PRODUCTS = "/api/products/search";
  private const string GET_PRODUCT = "/api/products/{id}";
  private const string POST_PRODUCTS = "/api/products";
  private const string PATCH_PRODUCTS = "/api/products/{id}";
  private const string ADD_ITEM = "/api/products/{id}/items";
  private const string UPDATE_ITEM = "/api/products/{id}/items/{itemId}";
  private const string REMOVE_ITEM = "/api/products/{id}/items/{itemId}";

  public static WebApplication SetupProductEndPoint(this WebApplication app)
  {
    app.MapPost(GET_PRODUCTS, async ([FromServices] IMediator mediator, [FromBody] SearchingProductRequest request) =>
    {
      var response = await mediator.Send(request);

      return Results.Ok(response);
    }).WithOpenApi()
      .WithTags(TAG_NAME);

    app.MapPost(POST_PRODUCTS, async ([FromServices] IMediator mediator, [FromBody] AddNewProductRequest request) =>
    {
      var response = await mediator.Send(request);

      if (response.Data == null)
        return Results.BadRequest(response.Error);

      return Results.Ok(response.Data);
    }).WithOpenApi()
      .WithTags(TAG_NAME)
      .RequireAuthorization("SuperUser");

    app.MapGet(GET_PRODUCT, async ([FromServices] IMediator mediator,
      [FromRoute] string id) =>
    {
      var request = new GetProductDetailRequest { Id = id };

      var response = await mediator.Send(request);

      return Results.Ok(response);
    }).WithOpenApi()
      .WithTags(TAG_NAME);

    app.MapPatch(PATCH_PRODUCTS, async ([FromServices] IMediator mediator,
      [FromRoute] string id,
      [FromBody] EditProductRequest request) =>
    {
      var payload = request with { Id =  id };

      var response = await mediator.Send(request);

      if(response.Error == null)
        return Results.Ok(response.Data);

      return Results.BadRequest(response.Error);
    }).WithOpenApi()
      .WithTags(TAG_NAME);

    app.MapPost(ADD_ITEM, async ([FromServices] IMediator mediator,
      [FromRoute] string id,
      [FromBody] AddNewProductItemRequest request) =>
    {
      var payload = request with { ProductId = id };

      var response = await mediator.Send(payload);

      return Results.Ok(response);
    }).WithOpenApi()
      .WithTags(TAG_NAME);

    app.MapPatch(UPDATE_ITEM, async ([FromServices] IMediator mediator,
      [FromRoute] string id,
      [FromRoute] string itemId,
      [FromBody] UpdateProductItemRequest request) =>
    {
      var payload = request with { ItemId = itemId, ProductId = id };

      var response = await mediator.Send(payload);

      return Results.Ok(response);
    }).WithOpenApi()
      .WithTags(TAG_NAME);

    app.MapDelete(REMOVE_ITEM, async ([FromServices] IMediator mediator,
      [FromRoute] string id,
      [FromRoute] string itemId) =>
    {
      var request = new RemoveProductItemRequest
      {
        ItemId = itemId,
        ProductId = id
      };

      var response = await mediator.Send(request);

      return Results.Ok(response);
    }).WithOpenApi()
      .WithTags(TAG_NAME);

    return app;
  }
}
