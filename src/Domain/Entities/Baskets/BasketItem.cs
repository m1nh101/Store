namespace Domain.Entities.Baskets;

public class BasketItem
{
  private double _price;
  private int _quantity;

  public string ProductId { get; set; } = string.Empty;
  public string ItemId { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public string Image { get; set; } = string.Empty;

  public double Price
  {
    get => _price;
    set
    {
      if(value < 0) throw new ArgumentOutOfRangeException(nameof(value));

      _price = value;
    }
  }

  public int Quantity
  {
    get => _quantity;
    set
    {
      if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value));

      _quantity = value;
    }
  }
}
