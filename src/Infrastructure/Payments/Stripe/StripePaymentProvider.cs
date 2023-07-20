using Domain.Entities.Baskets;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace Infrastructure.Payments.Stripe;

public sealed class StripePaymentProvider
{
  private readonly StripeOption _option;

  public StripePaymentProvider(IOptions<StripeOption> option)
  {
    _option = option.Value;
  }

  public Session Checkout(Basket basket)
  {
    var lineItems = basket.Items.Select(e => new SessionLineItemOptions
    {
      Quantity = e.Quantity,
      PriceData = new SessionLineItemPriceDataOptions
      {
        UnitAmountDecimal = Convert.ToDecimal(e.Price),
        Currency = "usd",
        ProductData = new SessionLineItemPriceDataProductDataOptions
        {
          Name = e.Name,
          Images = new List<string> { e.Image }
        },
      }
    }).ToList();

    var sessionOption = new SessionCreateOptions
    {
      LineItems = lineItems,
      Mode = "payment",
      SuccessUrl = "",
      CancelUrl = "",
      PaymentMethodTypes = new List<string> { "card" },
    };

    var client = new StripeClient(_option.ApiKey);

    var service = new SessionService(client);
    var session = service.Create(sessionOption);

    return session;
  }
}
