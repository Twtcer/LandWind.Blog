using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using LandWind.Blog.Core.Extensions;

namespace LandWind.Blog.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                      .ConfigureWebHostDefaults(builder =>
                      {
                          builder
                          .UseIISIntegration()
                          .UseStartup<Startup>();
                      })
                      .UseLog4Net()
                      .UseAutofac()
                      .Build()
                      .RunAsync();
        }
    }
}
