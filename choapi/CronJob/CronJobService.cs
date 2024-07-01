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

                        
                        // date expired off the promoted -1

                        var expiresPromotion = dbContext.Promotion.Where(p => p.Date_Promoted < DateTime.Now && p.Is_Active != true && p.Is_Deleted != false).ToList();

                        if (expiresPromotion.Any())
                        {
                            foreach (var promotion in expiresPromotion)
                            {
                                var establishment = dbContext.Establishment.FirstOrDefault(e => e.Establishment_Id == promotion.Establishment_Id);

                                if (establishment != null)
                                {
                                    if (establishment.Promo_Credit != null && establishment.Promo_Credit > 0)
                                    {
                                        establishment.Promo_Credit -= 1;
                                        establishment.Is_Promoted = false;

                                        dbContext.Establishment.Update(establishment);
                                        await dbContext.SaveChangesAsync();

                                        // check if the promoType Auto/Non-Auto
                                        // for auto credit 

                                        promotion.Is_Active = false;

                                        dbContext.Promotion.Update(promotion);
                                        await dbContext.SaveChangesAsync();
                                    }
                                }
                            }
                        }

                        var promotions = dbContext.Promotion.Where(p => p.Date_Promoted == DateTime.Now && p.Is_Active != false && p.Is_Deleted != false).ToList();

                        if (expiresPromotion.Any())
                        {
                            foreach (var promotion in expiresPromotion)
                            {
                                var establishment = dbContext.Establishment.FirstOrDefault(e => e.Establishment_Id == promotion.Establishment_Id);

                                if (establishment != null)
                                {
                                    if (establishment.Promo_Credit != null && establishment.Promo_Credit > 0)
                                    {
                                        establishment.Promo_Credit += 1;
                                        establishment.Is_Promoted = true;

                                        dbContext.Establishment.Update(establishment);
                                        await dbContext.SaveChangesAsync();

                                        // check if the promoType Auto/Non-Auto
                                        // for auto credit 

                                        promotion.Is_Active = true;

                                        dbContext.Promotion.Update(promotion);
                                        await dbContext.SaveChangesAsync();
                                    }
                                }
                            }
                        }

                        // date based to turn on promoted +1ch
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while executing the cron job.");
                }

                // Delay for 1 minutes before running again
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }

            _logger.LogInformation("Cron Job Service is stopping.");
        }
    }
}
