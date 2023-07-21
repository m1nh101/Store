using Application.Products.Edit;
using Application.Products.New;
using Application.Products.Searching;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public static class ProductEndpoint
{
  private const string TAG_NAME = "Product";
  private const string GET_PRODUCTS = "/api/products/search";
  private const string POST_PRODUCTS = "/api/products";
  private const string PATCH_PRODUCTS = "/api/products/{id}";

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
      .WithTags(TAG_NAME);

    app.MapPatch(PATCH_PRODUCTS, async ([FromServices] IMediator mediator,
      [FromRoute] string id,
      [FromBody] EditProductRequest request) =>
    {
      if(id != request.Id)
        return Results.BadRequest( new { Message = "payload not valid" });

      var response = await mediator.Send(request);

      if(response.Error == null)
        return Results.Ok(response.Data);

      return Results.BadRequest(response.Error);
    }).WithOpenApi()
      .WithTags(TAG_NAME);

    return app;
  }
}
