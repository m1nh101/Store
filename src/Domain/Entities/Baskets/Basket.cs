﻿using Domain.Exceptions;

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
    var existItem = Items.FirstOrDefault(e => e.ItemId == item.ItemId);

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
  public double RemoveItem(string itemId)
  {
    var existItem = Items.FirstOrDefault(e => e.ItemId == itemId)
      ?? throw new DomainException($"product id: {itemId} not found in basket");

    Items.Remove(existItem);

    Total = ReCalculateBasketPrice();
    return Total;
  }

  private double ReCalculateBasketPrice() => Items.Sum(e => e.Quantity * e.Price);
}
