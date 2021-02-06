using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace LandWind.Blog
{
    [DependsOn( 
        typeof(AbpIdentityApplicationModule)
        )]
    public class LandWindBlogApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        { 
        }
    }
}
