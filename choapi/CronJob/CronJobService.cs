using choapi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace choapi.CronJob
{
    public class CronJobService : BackgroundService
{
        private readonly ILogger<CronJobService> _logger;
        private readonly IServiceProvider _services;

        public CronJobService(IServiceProvider services, ILogger<CronJobService> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Cron Job Service is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Cron Job is running at: {time}", DateTimeOffset.Now);

                    // Perform your database operations here using DbContext
                    using (var scope = _services.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<ChoDBContext>();

                        // Example: Query data using DbContext

                        var random = new Random();
                        int randomId = random.Next(500, 1000);

                        var availability = new Availability
                        {
                            Establishment_Id = randomId,
                            Day = "M-F",
                            Time_Start = "9: 00 AM",
                            Time_End = "6:00 PM"
                        };

                        dbContext.Availability.Add(availability);
                        dbContext.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while executing the cron job.");
                }

                // Delay for 1 minutes before running again
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }

            _logger.LogInformation("Cron Job Service is stopping.");
        }
    }
}
