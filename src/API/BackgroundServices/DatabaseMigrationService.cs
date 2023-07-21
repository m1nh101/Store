using Infrastructure.Database;

namespace API.BackgroundServices;

public class DatabaseMigrationService : IHostedService
{
  private readonly DatabaseMigration _migration;
  private readonly ILogger<DatabaseMigrationService> _logger;

  public DatabaseMigrationService(DatabaseMigration migration,
    ILogger<DatabaseMigrationService> logger)
  {
    _migration = migration;
    _logger = logger;
  }

  public async Task StartAsync(CancellationToken cancellationToken)
  {
    _logger.LogInformation("running database migration");

    await _migration.Migrate();

    await StopAsync(cancellationToken);
  }

  public Task StopAsync(CancellationToken cancellationToken)
  {
    _logger.LogInformation($"Stop {nameof(DatabaseMigration)}");

    return Task.CompletedTask;
  }
}
