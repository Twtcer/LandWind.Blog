using Volo.Abp.Identity; 
using Volo.Abp.Modularity; 

namespace LandWind.Blog
{
    [DependsOn( 
        typeof(AbpIdentityDomainSharedModule)
        )]
    public class LandWindBlogDomainSharedModule : AbpModule
    {
       
    }
}
