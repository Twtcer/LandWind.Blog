using System.IO;
using LandWind.Blog.Core.Extensions;
using LandWind.Blog.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Autofac;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace LandWind.Blog.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(LandWindBlogApplicationModule),
        typeof(LandWindBlogEfCoreDbModule)
        )]
    public class LandWindBlogDbMigratorModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                      .AddYamlFile("appsettings.yml", true, true)
                                                      .Build(); 
            context.Services.Configure<AbpDbConnectionOptions>(options =>
            {
                var enableDb = config.GetSection("storage").GetValue<string>("enableDb");
                options.ConnectionStrings.Default = config.GetSection("storage").GetValue<string>(enableDb);
            });

        }
    }
}
