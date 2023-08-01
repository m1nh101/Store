using Domain.Abstracts;
using Domain.Enums;
using Domain.Specifications;
using Domain.ValueObjects;
using System.Linq;

namespace Domain.Entities.Products;

public class Product : Entity
{
  private Product()
  {
  }

  public Product(string name, string brand, double price)
  {
    Id = Identifier.Init();
    Name = name;
    Brand = brand;
    Price = price;
  }

  public string Name { get; private set; } = string.Empty;
  public string Brand { get; private set; } = string.Empty;
 
  public ProductState State { get; private set; } = ProductState.New;

  private double _price;
  public double Price
  {
    get
    {
      if (Sale == null)
        return _price;

      return _price - (_price * Sale.Value / 100);
    }
    set => _price = value;
  }

  private readonly List<ProductItem> _items = new();
  public IReadOnlyCollection<ProductItem> Items => _items.AsReadOnly();


  public ProductItem? GetItem(string id)
  {
    var specification = new GetProductItemByIdSpecification(id);

    return _items.FirstOrDefault(specification.IsSastifiedBy);
  }

  public void AddItems(params ProductItem[] items)
  {
    foreach(var item in items)
    {
      var specification = new ProductItemSpecification(item);

      var existItem = _items.FirstOrDefault(specification.IsSastifiedBy);

      if (existItem is null)
        _items.Add(item);
      else
      {
        existItem.ChangeQuantityTo(item.Quantity);
        existItem.ChangePriceTo(item.AdditionPrice);
      }
    }
  }

  public void RemoveItem(string id)
  {
    var item = _items.FirstOrDefault(e => e.Equals(Identifier.Init(id)));

    if (item is null)
      throw new NullReferenceException(nameof(item));

    _items.Remove(item);
  }

  public void ChangeStatusTo(ProductState state) => State = state;

  private readonly List<ProductImage> _images = new();
  public IReadOnlyCollection<ProductImage> Images => _images.AsReadOnly();

  public Identifier? SaleId { get; private set; } = null;
  public virtual Sale? Sale { get; private set; } = null;

  public void SetSaleCampain(Sale sale) => Sale = sale;

  public void Update(Product product)
  {
    Name = product.Name;
    Brand = product.Brand;
    Price = product.Price;
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