using Application.Users.Token;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationConfiguration
{
  public static IServiceCollection ConfigureApplication(this IServiceCollection services)
  {
    services.AddSingleton<JwtGenerator>();


    services.AddMediatR(opt =>
    {
      opt.RegisterServicesFromAssembly(typeof(ApplicationConfiguration).Assembly);
    });

    return services;
  }
}
