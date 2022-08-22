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

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Starting service...");

            cronusHost.Start();

            logger.LogInformation("Service started!");

            return Task.CompletedTask;
        }
    }
}
