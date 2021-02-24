using LandWind.Blog.Application;
using LandWind.Blog.Core.Caching;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace LandWind.Blog
{
    [DependsOn(  
        typeof(AbpIdentityApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(LandWindBlogApplicationCachingMoudle)
        )]
    public class LandWindBlogApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<LandWindBlogApplicationModule>(validate: true);
                options.AddProfile<LandWindBlogAutoMapperProfile>();
            });
        }
    }
}
