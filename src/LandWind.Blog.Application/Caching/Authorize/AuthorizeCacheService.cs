using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace LandWind.Blog.Application.Caching.Authorize
{
    public class AuthorizeCacheService : CachingServiceBase, IAuthorizeCacheService
    {
        public async Task AddAuthorizeCodeAsync(string code)
        {
            await Cache.SetStringAsync(ApplicationCachingConsts.CachePrefix.Authorize, code);
        }

        public async Task<string> GetAuthorizeCodeAsync()
        {
            return await Cache.GetStringAsync(ApplicationCachingConsts.CachePrefix.Authorize);
        }
    }
}
