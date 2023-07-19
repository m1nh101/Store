using Application.Contracts;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

    services.AddScoped<IStoreContext, StoreContext>();

    return services;
  }
}
