using API.Configurations;
using Application.Sales.New;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public static class SaleEndpoint
{
  private const string TagName = "Sale Campain";
  private const string PostSale = "/api/sales";

  public static WebApplication SetupSaleEndpoint(this WebApplication app)
  {
    app.MapPost(PostSale, async ([FromServices] IMediator mediator,
      HttpContext http,
      [FromBody] CreateSaleCampainRequest request) =>
    {
      var response = await mediator.Send(request);

      if (response.Error == null)
        return Results.Ok(response.Data);

      return Results.BadRequest(response.Error);

    }).RequireAuthorization(AuthorizePolicy.SuperUser)
      .WithOpenApi()
      .WithTags(TagName);

    return app;
  }
}
