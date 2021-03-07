using System;
using System.Threading;
using System.Threading.Tasks;
using LandWind.Blog.Application.DataSeed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp;

namespace LandWind.Blog.DbMigrator
{
    public class DbMigratorHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        public DbMigratorHostedService(IHostApplicationLifetime hostApplicationLifetime)
        {
            _hostApplicationLifetime =hostApplicationLifetime;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var application = AbpApplicationFactory.Create<LandWindBlogDbMigratorModule>(options =>
            {
                options.UseAutofac();
            });
            application.Initialize();

            Console.WriteLine("Executing database seed");

            {
                Console.WriteLine("Initialize user data...");
                await application.ServiceProvider
                            .GetRequiredService<UserDataSeedService>()
                            .SeedAsync();

                Console.WriteLine("Initialize blog data...");
                await application.ServiceProvider
                                 .GetRequiredService<BlogDataSeedService>()
                                 .SeedAsync(); 
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    }
}
