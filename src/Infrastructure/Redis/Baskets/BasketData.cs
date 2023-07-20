using Domain.Entities.Baskets;
using Redis.OM.Modeling;

namespace Infrastructure.Redis.Baskets;

[Document(StorageType = StorageType.Json)]
public sealed class BasketData : Basket
{
  [RedisIdField, Indexed]
  public override string UserId { get => base.UserId; set => base.UserId = value; }
}
