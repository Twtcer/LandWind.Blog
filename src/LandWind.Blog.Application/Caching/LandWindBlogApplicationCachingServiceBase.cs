using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LandWind.Blog.Core.Options;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Volo.Abp.DependencyInjection;

namespace LandWind.Blog.Application.Caching
{
    /// <summary>
    /// 缓存服务基类注入
    /// </summary>
    public class CachingServiceBase : ITransientDependency
    {
        private IDistributedCache _cache;
        protected readonly object ServiceProviderLock = new object();
        public IServiceProvider ServiceProvider { get; set; }
        public IOptions<StorageOptions> StorageOption { get; set; }
        protected IDistributedCache Cache => LazyGetRequiredService(ref _cache);
        protected TService LazyGetRequiredService<TService>(ref TService reference)
        {
            return LazyGetRequiredService(typeof(TService), ref reference);
        }

        public TRef LazyGetRequiredService<TRef>(Type serviceType, ref TRef reference)
        {
            if (reference == null)
            {
                lock (ServiceProviderLock)
                {
                    if (reference == null)
                    {
                        reference = (TRef)ServiceProvider.GetRequiredService(serviceType);
                    }
                }
            }

            return reference;
        }

        /// <summary>
        /// 移除键
        /// </summary>
        /// <returns></returns>
        public async Task RemoveAsync(string key)
        {
            if (key.IsNullOrWhiteSpace())
                throw new ArgumentException("缓存键不能为空或空字段", nameof(key));

            //var connectionMultiplexer = ConnectionMultiplexer.Connect(StorageOption.Value.Redis);
        }
        
    }
}
