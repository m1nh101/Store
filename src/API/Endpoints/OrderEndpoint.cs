using Application.Orders.Checkout;
using Application.Orders.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public static class OrderEndpoint
{
  private const string ORDER_TAG = "Order";
  private const string CREATE_ORDER = "/api/orders";
  private const string ORDER_PAYMENT_SUCCESS = "/api/orders/{id}?checkout=success";

  public static WebApplication SetupOrderEndpoint(this WebApplication app)
  {
    app.MapPost(CREATE_ORDER, async ([FromServices] IMediator mediator) =>
    {
      var request = new CreateOrderRequest();

      var response = await mediator.Send(request);

      return Results.Ok(response);
    }).RequireAuthorization("SignedInUser")
      .WithTags(ORDER_TAG);

    app.MapGet(ORDER_PAYMENT_SUCCESS, async ([FromServices] IMediator mediator, [FromRoute] int id) =>
    {
      var request = new OrderCheckoutRequest()
      {
        Id = id,
        State = Domain.Enums.OrderState.Shipping
      };

      var response = await mediator.Send(request);

      return Results.Ok(response);
    });

    return app;
  }
}
