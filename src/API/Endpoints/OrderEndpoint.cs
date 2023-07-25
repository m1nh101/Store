using Application.Orders.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public static class OrderEndpoint
{
  private const string ORDER_TAG = "Order";
  private const string CREATE_ORDER = "/api/orders";

  public static WebApplication SetupOrderEndpoint(this WebApplication app)
  {
    app.MapPost(CREATE_ORDER, async ([FromServices] IMediator mediator) =>
    {
      var request = new CreateOrderRequest();

      var response = await mediator.Send(request);

      return Results.Ok(response);
    }).RequireAuthorization("SignedInUser")
      .WithTags(ORDER_TAG);

    return app;
  }
}
