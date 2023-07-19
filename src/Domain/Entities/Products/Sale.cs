using Domain.Abstracts;
using Domain.ValueObjects;

namespace Domain.Entities.Products;

public sealed class Sale : Entity
{
  private Sale() { }

  public Sale(string name, DateTime start, DateTime end, short value)
  {
    Id = Identitifer.Init();
    Name = name;
    StartDate = start;
    EndDate = end;
    Value = value;
  }

  public Identitifer Id { get; private set; } = null!;
  public string Name { get; private set; } = string.Empty;
  public DateTime StartDate { get; private set; }
  public DateTime EndDate { get; private set; }
  public short Value { get; private set; }

  public ICollection<Product> Products { get; private set; } = new List<Product>();

  public void ApplyToProduct(params Product[] products)
  {
    foreach (var product in products)
      product.SetSaleCampain(this);
  }
}
