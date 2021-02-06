using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;

namespace LandWind.Blog
{
    [DependsOn(
        typeof(LandWindBlogDomainSharedModule),
        typeof(AbpObjectExtendingModule)
    )]
    public class BlogApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            BlogDtoExtensions.Configure();
        }
    }
}
