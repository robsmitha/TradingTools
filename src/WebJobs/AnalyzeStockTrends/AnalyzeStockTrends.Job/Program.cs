using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AnalyzeStockTrends.Job.Services;

namespace AnalyzeStockTrends.Job
{
    class Program
    {
        private static IServiceProvider _serviceProvider;
        public static IConfigurationRoot _configuration;

        const int ARG_DOWNTRENDS = 0;
        const int ARG_UPTRENDS = 1;

        /// <summary>
        /// Program to observe and report on candle stick patterns. (Downtrend reversals, Uptrend Continuations).
        /// </summary>
        static async Task Main(string[] args)
        {
            if (args == null) return;

            RegisterServices();

            IServiceScope scope = _serviceProvider.CreateScope();

            if (args.Length > ARG_DOWNTRENDS)
            {
                var downtrends = args[ARG_DOWNTRENDS].Split(",", StringSplitOptions.RemoveEmptyEntries); 
                var downtrendReversals = scope.ServiceProvider.GetRequiredService<DowntrendingReversals>();
                await downtrendReversals.Run(downtrends);
            }

            if(args.Length > ARG_UPTRENDS)
            {
                var uptrends = args[ARG_UPTRENDS].Split(",", StringSplitOptions.RemoveEmptyEntries);
                var uptrendContinuations = scope.ServiceProvider.GetRequiredService<UptrendingContinuation>();
                await uptrendContinuations.Run(uptrends);
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
            services.AddSingleton<IIEXClient, IEXClient>();
            services.AddSingleton<DowntrendingReversals>();
            services.AddSingleton<UptrendingContinuation>();
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
