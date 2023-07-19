namespace Domain.Entities.Baskets;

public class Basket
{
  public virtual string UserId { get; set; } = string.Empty;
  public double Total { get; set; } = 0;

  public List<BasketItem> Items = new();
}
