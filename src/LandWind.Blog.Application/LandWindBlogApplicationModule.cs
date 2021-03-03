using LandWind.Blog.Core;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper; 
using Volo.Abp.Modularity;

namespace LandWind.Blog
{
    [DependsOn(  
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(LandWindBlogCoreModule)
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
