using System;
using Elders.Cronus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;

namespace SimpleStartup
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            var logger = ConfigureLogging(services);

            try
            {
                CronusLogger.SetStartupLogger(logger); // inject the start up logger in order to get logs regarding Cronus discoveries and configuration issues

                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                services.AddSingleton<IConfiguration>(configuration);
                services.AddCronus(configuration);

                var serviceProvider = services.BuildServiceProvider();

                CronusBooter.BootstrapCronus(serviceProvider); // bootstrap Cronus at the end
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Configuration error! Failed to start application!");
            }
        }

        static Microsoft.Extensions.Logging.ILogger ConfigureLogging(IServiceCollection services)
        {
            var providers = new LoggerProviderCollection();

            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.Console()
               .WriteTo.Providers(providers)
               .CreateLogger();

            services.AddLogging(x => x.SetMinimumLevel(LogLevel.Trace).AddConsole());
            services.AddSingleton(providers);
            services.AddSingleton<ILoggerFactory>(sc =>
            {
                var providerCollection = sc.GetService<LoggerProviderCollection>();
                var factory = new SerilogLoggerFactory(null, true, providerCollection);

                foreach (var provider in sc.GetServices<ILoggerProvider>())
                    factory.AddProvider(provider);

                return factory;
            });

            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

            return logger;
        }
    }
}
