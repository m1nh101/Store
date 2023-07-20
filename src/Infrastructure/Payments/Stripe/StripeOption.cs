namespace Infrastructure.Payments.Stripe;

public class StripeOption
{
  public string ApiKey { get; set; } = string.Empty;
  public string SecretKey { get; set; } = string.Empty;
}
