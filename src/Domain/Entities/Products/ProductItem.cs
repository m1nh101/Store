using Domain.Abstracts;
using Domain.ValueObjects;

namespace Domain.Entities.Products;

public class ProductItem : Entity
{
  private ProductItem() {}

  public ProductItem(string color, string size, int quantity, double additionPrice = 0)
  {
    Id = Identifier.Init();
    Color = color;
    Size = size;
    Quantity = quantity;
    AdditionPrice = additionPrice;
  }

  public ProductItem(string id, int quantity, double additionPrice)
  {
    Id = Identifier.Init(id);
    Quantity = quantity;
    AdditionPrice = additionPrice;
  }

  public string Color { get; private set; } = null!;
  public string Size { get; private set; } = null!;
  public int Quantity { get; private set; }
  public double AdditionPrice { get; private set; }

  public void ChangeQuantityTo(int quantity)
  {
    if(quantity < 0)
      throw new ArgumentOutOfRangeException(nameof(quantity));

    Quantity = quantity;
  }

  public void ChangePriceTo(double price)
  {
    if(price < 0) 
      throw new ArgumentOutOfRangeException(nameof(price));

    AdditionPrice = price;
  }
}