using API.Contexts;
using Application.Contracts;

namespace API.Configurations;

public static class ApiConfiguration
{
  public static IServiceCollection ConfigureApiService(this IServiceCollection services)
  {
    services.AddScoped<IUserContext, UserContext>();

    return services;
  }
}
