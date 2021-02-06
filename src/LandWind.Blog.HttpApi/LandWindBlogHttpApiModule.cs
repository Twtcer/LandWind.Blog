using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace LandWind.Blog.HttpApi
{
    [DependsOn( 
        typeof(AbpIdentityHttpApiModule),
        typeof(LandWindBlogApplicationModule)
        )]
    public class LandWindBlogHttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            
        } 
    }
}
