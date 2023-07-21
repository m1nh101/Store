using Domain.Abstracts;
using Domain.Enums;
using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities.Products;

public class Product : Entity
{
  private Product()
  {
  }

  public Product(string name, string brand, double price)
  {
    Id = Identitifer.Init();
    Name = name;
    Brand = brand;
    Price = price;
  }

  public Identitifer Id { get; private set; } = null!;
  public string Name { get; private set; } = string.Empty;
  public string Brand { get; private set; } = string.Empty;
  public double Price { get; private set; }
  public int Stock { get; private set; }
  public ProductState State { get; private set; } = ProductState.New;

  public void ChangeStatusTo(ProductState state) => State = state;

  private readonly List<ProductImage> _images = new();
  public IReadOnlyCollection<ProductImage> Images => _images.AsReadOnly();

  public Identitifer? SaleId { get; private set; } = null;
  public virtual Sale? Sale { get; private set; } = null;

  public void SetSaleCampain(Sale sale) => Sale = sale;

  public void Update(Product product)
  {
    Name = product.Name;
    Brand = product.Brand;
    Price = product.Price;
  }

  public void UpdateStock(int stock)
  {
    if (stock < 0)
      throw new DomainException("stock can be negative");

    Stock = stock;
  }

  public void AddImages(params string[] urls)
  {
    foreach (var url in urls)
      _images.Add(new ProductImage { Url = url });
  }

  public void RemoveImages(params string[] urls)
  {
    foreach (var url in urls)
      _images.Remove(new ProductImage { Url = url });
  }
}