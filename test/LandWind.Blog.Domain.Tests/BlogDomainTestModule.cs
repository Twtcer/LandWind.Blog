using LandWind.Blog.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace LandWind.Blog
{
    [DependsOn(
        typeof(BlogEntityFrameworkCoreTestModule)
        )]
    public class BlogDomainTestModule : AbpModule
    {

    }
}