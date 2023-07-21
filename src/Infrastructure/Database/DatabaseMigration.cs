using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Database;

public class DatabaseMigration
{
  private readonly IServiceProvider _provider;

  public DatabaseMigration(IServiceProvider provider)
  {
    _provider = provider;
  }

  public async Task Migrate()
  {
    using var scope = _provider.CreateScope();

    var context = scope.ServiceProvider.GetService<StoreContext>()!;

    var pendingMigrations = await context.Database.GetPendingMigrationsAsync();

    if (pendingMigrations.Any())
      await context.Database.MigrateAsync();
  }
}
