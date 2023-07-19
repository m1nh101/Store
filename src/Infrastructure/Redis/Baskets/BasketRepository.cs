using Redis.OM.Contracts;
using Redis.OM.Searching;

namespace Infrastructure.Redis.Baskets;

public sealed class BasketRepository
{
  private readonly IRedisConnectionProvider _provider;
  private readonly IRedisCollection<BasketData> _baskets;

  public BasketRepository(IRedisConnectionProvider provider)
  {
    _provider = provider;
    _baskets = _provider.RedisCollection<BasketData>();
  }
}
