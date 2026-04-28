using Elders.Cronus;

namespace Cronus.Sample.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ICronusHost cronusHost;
        private readonly ILogger<Worker> logger;

        public Worker(ICronusHost cronusHost, ILogger<Worker> logger)
        {
            this.cronusHost = cronusHost;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Starting service...");

            await cronusHost.StartAsync();

            logger.LogInformation("Service started!");
        }
    }
}
