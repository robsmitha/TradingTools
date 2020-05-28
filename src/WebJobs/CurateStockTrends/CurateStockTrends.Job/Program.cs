using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CurateStockTrends.Job
{
    class Program
    {
        private static IServiceProvider _serviceProvider;
        public static IConfigurationRoot _configuration;

        const int ARG_STOCKS = 0;

        /// <summary>
        /// Program to organize a list of stocks into uptrends and downtrends for a given range
        /// </summary>
        /// <param name="args"></param>
        static async Task Main(string[] args)
        {
            if (args == null) return;

            RegisterServices();

            IServiceScope scope = _serviceProvider.CreateScope();

            if (args.Length > ARG_STOCKS)
            {
                var stocks = args[ARG_STOCKS].Split(",", StringSplitOptions.RemoveEmptyEntries);
                var curateStocks = scope.ServiceProvider.GetRequiredService<CurateStocks>();
                await curateStocks.Run(stocks);
            }

            DisposeServices();
        }
        static void RegisterServices()
        {
            Console.OutputEncoding = Encoding.UTF8;

            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (string.IsNullOrWhiteSpace(environment))
                throw new ArgumentNullException("Environment not found in ASPNETCORE_ENVIRONMENT");

            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(environment == "Development" ? $"appsettings.{environment}.json" : "appsettings.json", optional: false);

            _configuration = builder.Build();

            var services = new ServiceCollection();
            //services.AddSingleton<IIEXClient, IEXClient>();
            services.AddSingleton<CurateStocks>();

            services.AddSingleton<IConfiguration>(provider => _configuration);

            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("log.txt")
            .CreateLogger();

            _serviceProvider = services.BuildServiceProvider(true);

            Log.Information($"Environment: {environment}");
        }

        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
