using API.Configurations;
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
  private const string OrderTag = "Order";
  private const string CreateOrder = "/api/orders";
  private const string OrderPaymentSuccess = "/api/orders/{id}/success";
  private const string GetOrderByUser = "/api/orders";
  private const string GetOrder = "/api/admin/orders";
  private const string GetOrderDetail = "/api/orders/{id}";

  public static WebApplication SetupOrderEndpoint(this WebApplication app)
  {
    app.MapPost(CreateOrder, async ([FromServices] IMediator mediator) =>
    {
      var request = new CreateOrderRequest();

      var response = await mediator.Send(request);

      return Results.Ok(response);
    }).WithOpenApi()
      .RequireAuthorization(AuthorizePolicy.SignedIn)
      .WithTags(OrderTag);

    app.MapGet(OrderPaymentSuccess, async ([FromServices] IMediator mediator, [FromRoute] int id) =>
    {
      var request = new OrderCheckoutRequest()
      {
        Id = id,
        State = OrderState.Shipping
      };

      var response = await mediator.Send(request);

      return Results.Ok(response);
    }).WithTags(OrderTag)
      .WithOpenApi()
      .RequireAuthorization(AuthorizePolicy.SignedIn);

    app.MapGet(GetOrderByUser, async ([FromServices] IMediator mediator,
      [FromQuery] OrderState state) =>
    {
      var request = new GetUserOrderRequest()
      {
        State = state,
      };

      var response = await mediator.Send(request);

      return Results.Ok(response.Data);
    }).WithOpenApi()
      .WithTags(OrderTag)
      .RequireAuthorization(AuthorizePolicy.SignedIn);

    app.MapGet(GetOrder, async ([FromServices] IMediator mediator,
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
      .WithTags(OrderTag)
      .RequireAuthorization(AuthorizePolicy.SuperUser);

    app.MapGet(GetOrderDetail, async ([FromServices] IMediator mediator,
      [FromRoute] int id) =>
    {
      var request = new GetOrderDetailRequest() { Id = id };

      var response = await mediator.Send(request);

      return Results.Ok(response);

    }).WithOpenApi()
      .WithTags(OrderTag)
      .RequireAuthorization(AuthorizePolicy.SignedIn);

    return app;
  }
}
