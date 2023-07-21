using Application.Sales.New;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public static class SaleEndpoint
{
  private const string TAG_NAME = "Sale Campain";
  private const string POST_SALE = "/api/sales";

  public static WebApplication SetupSaleEndpoint(this WebApplication app)
  {
    app.MapPost(POST_SALE, async ([FromServices] IMediator mediator,
      HttpContext http,
      [FromBody] CreateSaleCampainRequest request) =>
    {
      var response = await mediator.Send(request);

      if (response.Error == null)
        return Results.Ok(response.Data);

      return Results.BadRequest(response.Error);

    }).RequireAuthorization("SuperUser")
      .WithOpenApi()
      .WithTags(TAG_NAME);

    return app;
  }
}
