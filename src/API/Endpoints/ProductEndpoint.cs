using API.Configurations;
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
  private const string TagName = "Product";
  private const string GetProducts = "/api/products/search";
  private const string GetProduct = "/api/products/{id}";
  private const string PostProducts = "/api/products";
  private const string PatchProducts = "/api/products/{id}";
  private const string AddItem = "/api/products/{id}/items";
  private const string UpdateItem = "/api/products/{id}/items/{itemId}";
  private const string RemoveItem = "/api/products/{id}/items/{itemId}";

  public static WebApplication SetupProductEndPoint(this WebApplication app)
  {
    app.MapPost(GetProducts, async ([FromServices] IMediator mediator, [FromBody] SearchingProductRequest request) =>
    {
      var response = await mediator.Send(request);

      return Results.Ok(response);
    }).WithOpenApi()
      .WithTags(TagName);

    app.MapPost(PostProducts, async ([FromServices] IMediator mediator, [FromBody] AddNewProductRequest request) =>
    {
      var response = await mediator.Send(request);

      if (response.Data == null)
        return Results.BadRequest(response.Error);

      return Results.Ok(response.Data);
    }).WithOpenApi()
      .WithTags(TagName)
      .RequireAuthorization(AuthorizePolicy.SuperUser);

    app.MapGet(GetProduct, async ([FromServices] IMediator mediator,
      [FromRoute] string id) =>
    {
      var request = new GetProductDetailRequest { Id = id };

      var response = await mediator.Send(request);

      return Results.Ok(response);
    }).WithOpenApi()
      .WithTags(TagName);

    app.MapPatch(PatchProducts, async ([FromServices] IMediator mediator,
      [FromRoute] string id,
      [FromBody] EditProductRequest request) =>
    {
      var payload = request with { Id =  id };

      var response = await mediator.Send(request);

      if(response.Error == null)
        return Results.Ok(response.Data);

      return Results.BadRequest(response.Error);
    }).WithOpenApi()
      .WithTags(TagName)
      .RequireAuthorization(AuthorizePolicy.SuperUser);

    app.MapPost(AddItem, async ([FromServices] IMediator mediator,
      [FromRoute] string id,
      [FromBody] AddNewProductItemRequest request) =>
    {
      var payload = request with { ProductId = id };

      var response = await mediator.Send(payload);

      return Results.Ok(response);
    }).WithOpenApi()
      .WithTags(TagName)
      .RequireAuthorization(AuthorizePolicy.SuperUser);

    app.MapPatch(UpdateItem, async ([FromServices] IMediator mediator,
      [FromRoute] string id,
      [FromRoute] string itemId,
      [FromBody] UpdateProductItemRequest request) =>
    {
      var payload = request with { ItemId = itemId, ProductId = id };

      var response = await mediator.Send(payload);

      return Results.Ok(response);
    }).WithOpenApi()
      .WithTags(TagName)
      .RequireAuthorization(AuthorizePolicy.SuperUser);

    app.MapDelete(RemoveItem, async ([FromServices] IMediator mediator,
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
      .WithTags(TagName)
      .RequireAuthorization(AuthorizePolicy.SuperUser);

    return app;
  }
}
