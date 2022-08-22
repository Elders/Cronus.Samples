using Elders.Cronus;
using Cronus.Sample.Worker;

CreateHostBuilder(args).Build().Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(x => x.AddEnvironmentVariables())
            .ConfigureServices((hostContext, services) =>
            {
                services.AddOptions();
                services.AddLogging();
                services.AddHostedService<Worker>();

                services.AddCronus(hostContext.Configuration);
            });