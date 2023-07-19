using Domain.Entities.Products;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Contracts;

public interface IStoreContext
{
  DbSet<Product> Products { get; }
  DbSet<User> Users { get; }
  DbSet<Sale> Sales { get; }
  Task Commit(CancellationToken cancellationToken = default);
}
