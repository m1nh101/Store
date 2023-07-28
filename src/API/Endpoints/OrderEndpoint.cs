using Application.Orders.Checkout;
using Application.Orders.Create;
using Application.Orders.Get.AllOrders;
using Application.Orders.Get.Detail;
using Application.Orders.Get.UserOrders;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public static class OrderEndpoint
{
  private const string ORDER_TAG = "Order";
  private const string CREATE_ORDER = "/api/orders";
  private const string ORDER_PAYMENT_SUCCESS = "/api/orders/{id}/success";
  private const string GET_ORDER_BY_USER = "/api/orders";
  private const string GET_ORDER = "/api/admin/orders";
  private const string GET_ORDER_DETAIL = "/api/orders/{id}";

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
    }).WithTags(ORDER_TAG);

    app.MapGet(GET_ORDER_BY_USER, async ([FromServices] IMediator mediator,
      [FromQuery] OrderState state) =>
    {
      var request = new GetUserOrderRequest()
      {
        State = state,
      };

      var response = await mediator.Send(request);

      return Results.Ok(response.Data);
    }).WithOpenApi()
      .WithTags(ORDER_TAG)
      .RequireAuthorization("SignedInUser");

    app.MapGet(GET_ORDER, async ([FromServices] IMediator mediator,
      [FromQuery] OrderState state,
      [FromQuery] DateTime date) =>
    {
      var request = new GetAllOrderRequest
      {
        State = state,
        Date = date,
      };

      var response = await mediator.Send(request);

      return Results.Ok(response.Data);
    }).WithOpenApi()
      .WithTags(ORDER_TAG)
      .RequireAuthorization("SuperUser");

    app.MapGet(GET_ORDER_DETAIL, async ([FromServices] IMediator mediator,
      [FromRoute] int id) =>
    {
      var request = new GetOrderDetailRequest() { Id = id };

      var response = await mediator.Send(request);

      return Results.Ok(response);

    }).WithOpenApi()
      .WithTags(ORDER_TAG)
      .RequireAuthorization("SignedInUser");

    return app;
  }
}
