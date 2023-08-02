using Application.Contracts;
using Domain.Entities.Baskets;
using Domain.Entities.Products;
using Domain.Events;
using MediatR;

namespace Application.Products.EventHandlers;

public sealed class ProductPriceHasChangedEventHandler : INotificationHandler<ProductPriceHasChangedEvent>
{
  private readonly IBasketRepository _baskets;

  public ProductPriceHasChangedEventHandler(IBasketRepository baskets)
  {
    _baskets = baskets;
  }

  public async Task Handle(ProductPriceHasChangedEvent notification, CancellationToken cancellationToken)
  {
    var baskets = await _baskets.GetAll();

    foreach(var basket in baskets)
    {
      var isBasketHaveItem = basket.Items.Any(e => e.ProductId == notification.Product.Id.ToString());

      if (!isBasketHaveItem) continue;

      ReCalculateItemPriceInBasket(basket, notification.Product);

      await _baskets.Save(basket);
    }
  }

  static void ReCalculateItemPriceInBasket(Basket basket, Product product)
  {
    foreach (var item in basket.Items)
    {
      var productItem = product.Items.FirstOrDefault(e => e.Id.ToString() == item.ItemId);

      if (productItem is null)
        throw new NullReferenceException(nameof(productItem));

      item.Price = product.Price + productItem.AdditionPrice;
    }
  }
}
