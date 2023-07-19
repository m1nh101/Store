using Infrastructure.CronJobs;
using Quartz;

namespace API.Configurations;

public static class CronJobConfiguration
{
  public static IServiceCollection ConfigureCronJob(this IServiceCollection services)
  {

    services.AddQuartz(opt =>
    {
      opt.UseMicrosoftDependencyInjectionJobFactory();

      var clearSaleCampainJobKey = new JobKey("clearSaleCampain");

      opt.AddJob<SaleCronJob>(opt => opt.WithIdentity(clearSaleCampainJobKey));

      opt.AddTrigger(opt => opt.ForJob(clearSaleCampainJobKey)
        .WithIdentity("clearSaleCampainTrigger")
        .WithCronSchedule("0 0 * * * ?")); // run every day at 00:00

    });

    services.AddQuartzHostedService(opt =>
    {
      opt.WaitForJobsToComplete = true;
    });


    return services;
  }
}
