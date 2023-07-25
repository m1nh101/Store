using Application.Common;
using Application.Contracts;
using Domain.Abstracts;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using Domain.Entities.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class StoreContext : DbContext, IStoreContext
{
  private readonly IMediator _mediator;

  public StoreContext(DbContextOptions<StoreContext> option,
    IMediator mediator)
    : base(option)
  {
    _mediator = mediator;
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreContext).Assembly);
  }

  public async Task Commit(CancellationToken cancellationToken = default)
  {
    foreach (var entry in ChangeTracker.Entries<Entity>())
    {
      entry.Entity.LastTimeModified = DateTime.UtcNow;
    }

    foreach (var entry in ChangeTracker.Entries<AggregateRoot>())
    {
      var events = entry.Entity.Events;

      foreach (var @event in events)
        await _mediator.Publish(@event, cancellationToken);
    }

    await base.SaveChangesAsync(cancellationToken);
  }

  public DbSet<Product> Products => Set<Product>();
  public DbSet<User> Users => Set<User>();
  public DbSet<Sale> Sales => Set<Sale>();
  public DbSet<Order> Orders => Set<Order>();
}
