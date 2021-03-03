using System;
using System.Threading.Tasks; 
using LandWind.Blog.Core.Caching;
using LandWind.Blog.Core.Response.Base;
using static LandWind.Blog.Application.Caching.LandWindBlogApplicationCachingConsts;

namespace LandWind.Blog.Application.Caching.Authorize
{
    public class AuthorizeCacheService : CachingServiceBase, IAuthorizeCacheService
    {
        private const string GetLoginAddressKey = "Authorize:GetLoginAddress";
        private const string GetAccessTokenKey = "Authorize:GetAccessToken-{0}";
        private const string GenerateTokenKey = "Authorize:GenerateToken-{0}";

        public async Task<ResponseResult<string>> GenerateTokenAsync(string access_token, Func<Task<ResponseResult<string>>> factory)
        {
            return await Cache.GetOrAddAsync(string.Format(GenerateTokenKey, access_token), factory, CacheStrategy.OneHours);
        }

        public async Task<ResponseResult<string>> GetAccessTokenAsync(string code, Func<Task<ResponseResult<string>>> factory)
        {
            return await Cache.GetOrAddAsync(string.Format(GetAccessTokenKey, code), factory, CacheStrategy.OneMinute);
        }

        public async Task<ResponseResult<string>> GetLoginAddressAsync(Func<Task<ResponseResult<string>>> factory)
        {
            return await Cache.GetOrAddAsync(GetLoginAddressKey, factory, CacheStrategy.Never);
        }
    }
}
