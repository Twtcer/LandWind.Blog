using Volo.Abp.Modularity;

namespace LandWind.Blog
{
    [DependsOn(
        typeof(BlogApplicationModule),
        typeof(BlogDomainTestModule)
        )]
    public class BlogApplicationTestModule : AbpModule
    {

    }
}