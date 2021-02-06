using LandWind.Blog.Domain;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;

namespace LandWind.Blog.Application.Caching
{
    [DependsOn(
        typeof(AbpCachingModule),
        typeof(LandWindBlogDomainModule)
        )]
    public class LandWindBlogApplicationCachingMoudle:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);    
        }
    }
}
