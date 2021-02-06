using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace LandWind.Blog.Domain
{
    [DependsOn(
        typeof(LandWindBlogDomainSharedModule), 
        typeof(AbpIdentityDomainModule)
    )]
    public class LandWindBlogDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        { 
        }
    }
}
