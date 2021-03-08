using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using LandWind.Blog.Core.Extensions;
using Serilog;
using Serilog.Events;

namespace LandWind.Blog.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                 .Enrich.FromLogContext()
                 .WriteTo.Async(c => c.File("Logs/logs.txt"))
#if DEBUG
                .WriteTo.Async(c => c.Console())
#endif
                .CreateLogger();
            try
            {
                Log.Information("Application Starting...");
                await CreateHostBuilder(args)
                    .Build()  
                    .RunAsync();
            }
            catch (System.Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly!");
            }
            finally
            {
                Log.CloseAndFlush();
                Log.Information("Application Stoped!");
            } 
        }

        internal static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddYamlFile("appsettings.yml",
                    optional: true,
                    reloadOnChange: true
                    );
                var configDict = config.Build().ToDictionary();
                //Log.Information("appsettings {@appsettings}", configDict);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .UseAutofac()
            .UseSerilog();
    }
}
