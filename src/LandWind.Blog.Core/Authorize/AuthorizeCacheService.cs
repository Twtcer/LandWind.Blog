using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LandWind.Blog.Core.Caching;
using LandWind.Blog.Domain.Shared;
using LandWind.Blog.Domain.Shared.Base;

namespace LandWind.Blog.Core.Authorize
{
    public class AuthorizeCacheService : CachingServiceBase, IAuthorizeCacheService
    {
        private const string GetLoginAddressKey = "Authorize:GetLoginAddress";
        private const string GetAccessTokenKey = "Authorize:GetAccessToken-{0}";
        private const string GenerateTokenKey = "Authorize:GenerateToken-{0}";

        public async Task<ResponseResult<string>> GenerateTokenAsync(string access_token, Func<Task<ResponseResult<string>>> factory)
        {
            return await Cache.GetOrAddAsync(string.Format(GetLoginAddressKey, access_token), factory, CacheStrategy.OneHours);
        }

        public async Task<ResponseResult<string>> GetAccessTokenAsync(string code, Func<Task<ResponseResult<string>>> factory)
        {
            return await Cache.GetOrAddAsync(string.Format(GetAccessTokenKey, code), factory, CacheStrategy.FiveMinute);
        }

        public async Task<ResponseResult<string>> GetLoginAddressAsync(Func<Task<ResponseResult<string>>> factory)
        {
            return await Cache.GetOrAddAsync(GetLoginAddressKey, factory, CacheStrategy.Never);
        }
    }
}
