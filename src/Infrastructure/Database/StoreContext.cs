using Application.Contracts;
using Domain.Abstracts;
using Domain.Entities.Products;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class StoreContext : DbContext, IStoreContext
{
  public StoreContext(DbContextOptions<StoreContext> option)
    : base(option)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreContext).Assembly);
  }

  public Task Commit(CancellationToken cancellationToken = default)
  {
    foreach (var entry in ChangeTracker.Entries<Entity>())
    {
      entry.Entity.LastTimeModified = DateTime.UtcNow;
    }

    return base.SaveChangesAsync(cancellationToken);
  }

  public DbSet<Product> Products => Set<Product>();

  public DbSet<User> Users => Set<User>();
  public DbSet<Sale> Sales => Set<Sale>();
}
