using Volo.Abp.Modularity;

namespace LandWind.Blog
{
    [DependsOn(
        typeof(LandWindBlogApplicationModule),
        typeof(BlogDomainTestModule)
        )]
    public class BlogApplicationTestModule : AbpModule
    {

    }
}