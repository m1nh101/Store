using Infrastructure.Redis.Baskets;
using Redis.OM;
using Redis.OM.Contracts;

namespace API.BackgroundServices;

public sealed class RedisIndexCreationService : IHostedService
{
  private readonly IRedisConnectionProvider _provider;

  public RedisIndexCreationService(IRedisConnectionProvider provider)
  {
    _provider = provider;
  }

  public async Task StartAsync(CancellationToken cancellationToken)
  {
    await _provider.Connection.CreateIndexAsync(typeof(BasketData));
  }

  public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
