using Application.Contracts;
using Domain.Entities.Baskets;
using Redis.OM;
using Redis.OM.Contracts;
using Redis.OM.Searching;

namespace Infrastructure.Redis.Baskets;

public sealed class BasketRepository : IBasketRepository
{
  private readonly IRedisConnectionProvider _provider;
  private readonly IRedisCollection<BasketData> _baskets;

  public BasketRepository(IRedisConnectionProvider provider)
  {
    _provider = provider;
    _baskets = _provider.RedisCollection<BasketData>();
  }

  public async Task<Basket> Get(string userId)
  {
    var basket = await _baskets.FindByIdAsync(userId);

    if (basket is null)
      return new Basket() { UserId = userId };

    return basket;
  }

  public void RemoveBasket(string userId) => _provider.Connection.Unlink($"Basket:{userId}");

  public async Task Save(Basket source)
  {
    var basket = new BasketData()
    {
      Items = source.Items,
      UserId = source.UserId,
      Total = source.Total,
    };

    await _baskets.InsertAsync(basket);
  }
}
