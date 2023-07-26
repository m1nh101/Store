using Domain.Entities.Baskets;
using Stripe.Checkout;

namespace Infrastructure.Payment;

public sealed class CreateStripePaymentSession
{
  private const string PAYMENT_MODE = "payment";
  private const string PAYMENT_SUCCESS_API = "";
  private const string PAYMENT_FAIL_API = "";
  private const string PAYMENT_CURRENCY = "VND";
  private readonly List<string> _paymentModeTypes;

  public CreateStripePaymentSession()
  {
    _paymentModeTypes = new() { "card" };
  }

  public async Task<string> GetCheckoutSession(Basket basket)
  {
    var lineItems = basket.Items.Select(e => new SessionLineItemOptions
    {
      Quantity = e.Quantity,
      PriceData = new SessionLineItemPriceDataOptions
      {
        UnitAmountDecimal = Convert.ToDecimal(e.Price),
        Currency = PAYMENT_CURRENCY,
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
      Mode = PAYMENT_MODE,
      SuccessUrl = PAYMENT_SUCCESS_API,
      CancelUrl = PAYMENT_FAIL_API,
      PaymentMethodTypes = _paymentModeTypes,
    };

    var service = new SessionService();

    var session = await service.CreateAsync(sessionOption);

    return session.Url;
  }
}
