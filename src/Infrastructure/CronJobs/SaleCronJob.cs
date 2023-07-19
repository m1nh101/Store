using Application.Contracts;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace Infrastructure.CronJobs;

public sealed class SaleCronJob : IJob
{
  private readonly IStoreContext _context;

  public SaleCronJob(IStoreContext context)
  {
    _context = context;
  }

  public async Task Execute(IJobExecutionContext context)
  {
    var campains = await _context.Sales
      .AsNoTracking()
      .Where(e => e.EndDate > DateTime.UtcNow)
      .Select(e => e.Id)
      .ToListAsync();

    await _context.Products
      .Where(d => campains.Any(e => e.Equals(d.SaleId)))
      .ExecuteUpdateAsync(e => e.SetProperty(d => d.SaleId, e => null));
  }
}
