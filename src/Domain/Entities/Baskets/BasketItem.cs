namespace Domain.Entities.Baskets;

public class BasketItem
{
  public string ProductId { get; set; } = string.Empty;
  public string ItemId { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public double Price { get; set; }
  public int Quantity { get; set; }
  public string Image { get; set; } = string.Empty;
}
