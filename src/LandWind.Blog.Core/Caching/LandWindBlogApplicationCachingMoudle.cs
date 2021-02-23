using LandWind.Blog.Domain;
using LandWind.Blog.Domain.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;

namespace LandWind.Blog.Core.Caching
{
    [DependsOn(
        typeof(AbpCachingModule),
        typeof(LandWindBlogDomainModule)
        )]
    public class LandWindBlogApplicationCachingMoudle:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Appsettings.Caching.RedisConnectionString;
               // options.InstanceName = "";
               //options.ConfigurationOptions = 
            });
        }
    }
}
