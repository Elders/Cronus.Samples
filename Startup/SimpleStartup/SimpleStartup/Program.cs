using Elders.Cronus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleStartup
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var services = new ServiceCollection();
            services.AddCronus(configuration);

            var serviceProvider = services.BuildServiceProvider();
            CronusBooter.BootstrapCronus(serviceProvider);
        }
    }
}
