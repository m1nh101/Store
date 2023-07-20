namespace Domain.Entities.Baskets;

public class Basket
{
  public virtual string UserId { get; set; } = string.Empty;
  public double Total { get; set; } = 0;

  public List<BasketItem> Items { get; set; } = new();

  /// <summary>
  /// add item to current basket
  /// </summary>
  /// <param name="item"></param>
  /// <returns>total basket price after item change</returns>
  public double AddItem(BasketItem item)
  {
    var existItem = Items.FirstOrDefault(e => e.ProductId == item.ProductId);

    if (existItem == null)
      Items.Add(item);
    else
      existItem.Quantity = item.Quantity;

    Total = ReCalculateBasketPrice();
    return Total;
  }

  /// <summary>
  /// remove item from current basket
  /// </summary>
  /// <param name="item"></param>
  /// <returns>total basket price after item change</returns>
  public double RemoveItem(string productId)
  {
    var existItem = Items.FirstOrDefault(e => e.ProductId == productId) 
      ?? throw new NullReferenceException(nameof(productId));

    Items.Remove(existItem);

    Total = ReCalculateBasketPrice();
    return Total;
  }

  private double ReCalculateBasketPrice() => Items.Sum(e => e.Quantity * e.Price);
}
