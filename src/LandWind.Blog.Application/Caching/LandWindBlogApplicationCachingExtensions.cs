using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LandWind.Blog.Core.Extensions;
using LandWind.Blog.Domain.Shared;
using Microsoft.Extensions.Caching.Distributed;
using static LandWind.Blog.Application.Caching.LandWindBlogApplicationCachingConsts;

namespace LandWind.Blog.Core.Caching
{
    public static class LandWindBlogApplicationCachingExtensions
    {
        /// <summary>
        /// 获取缓存对象，不存在则添加
        /// </summary>
        /// <typeparam name="TCacheItem"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="factory"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public static async Task<TCacheItem> GetOrAddAsync<TCacheItem>(this IDistributedCache cache, string key, Func<Task<TCacheItem>> factory, int minutes)
        {
            TCacheItem cacheItem;

            var result = await cache.GetStringAsync(key);
            if (string.IsNullOrEmpty(result))
            {
                cacheItem = await factory.Invoke();

                var options = new DistributedCacheEntryOptions();
                if (minutes != CacheStrategy.Never)
                {
                    options.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(minutes);
                }
                await cache.SetStringAsync(key, cacheItem.SerializeToJson(), options);
            }
            else
            {
                cacheItem = result.DeserializeToObject<TCacheItem>();
            }

            return cacheItem;
        }
    }
}
