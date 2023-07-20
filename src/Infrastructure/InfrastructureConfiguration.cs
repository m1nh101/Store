using Application.Contracts;
using Infrastructure.Database;
using Infrastructure.Redis.Baskets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Redis.OM;
using Redis.OM.Contracts;

namespace Infrastructure;

public static class InfrastructureConfiguration
{
  public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddDbContext<StoreContext>(opt =>
    {
      opt.UseSqlServer(configuration.GetConnectionString("StoreConn"),
        x => x.MigrationsAssembly(typeof(InfrastructureConfiguration).Assembly.FullName));
    });

    services.AddSingleton<IRedisConnectionProvider>(sp =>
    {
      var connection = new RedisConnectionConfiguration
      {
        Host = configuration["Redis:Host"] ?? "localhost",
        Port = Convert.ToInt32(configuration["Redis:Port"] ?? "6379")
      };

      return new RedisConnectionProvider(connection);
    });

    services.AddScoped<IStoreContext, StoreContext>();
    services.AddScoped<IBasketRepository, BasketRepository>();

    return services;
  }
}
