using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp.DependencyInjection;

namespace LandWind.Blog.Core.Authorize
{
    public class CachingServiceBase : ITransientDependency
    {
        public IDistributedCache Cache { get; set; }
    }
}
