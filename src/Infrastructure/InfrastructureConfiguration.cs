using Application.Contracts;
using Infrastructure.Database;
using Infrastructure.Payment;
using Infrastructure.Redis.Baskets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Redis.OM;
using Redis.OM.Contracts;
using Stripe;

namespace Infrastructure;

public static class InfrastructureConfiguration
{
  public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddDbContext<StoreContext>(opt =>
    {
      var isDockerHost = !string.IsNullOrEmpty(configuration["DatabaseServer"]);
      string? connection;

      if (isDockerHost)
        connection = $"Server={configuration["DatabaseServer"]};UID=sa;PWD={configuration["DatabasePassword"]};Database=StoreDb;Encrypt=False";
      else
        connection = configuration.GetConnectionString("StoreConn");

      opt.UseSqlServer(connection,
      x => x.MigrationsAssembly(typeof(InfrastructureConfiguration).Assembly.FullName));
    });

    services.AddSingleton<IRedisConnectionProvider>(sp =>
    {
      var connection = new RedisConnectionConfiguration
      {
        Host = configuration["Redis:Host"] ?? configuration["RedisHost"] ?? "localhost",
        Port = Convert.ToInt32(configuration["Redis:Port"] ?? "6379")
      };

      return new RedisConnectionProvider(connection);
    });

    StripeConfiguration.ApiKey = configuration["Stripe:ApiKey"];

    services.AddScoped<IStoreContext, StoreContext>();
    services.AddScoped<IBasketRepository, BasketRepository>();

    services.AddSingleton<DatabaseMigration>();

    services.AddScoped<CreateStripePaymentSession>();

    return services;
  }
}
