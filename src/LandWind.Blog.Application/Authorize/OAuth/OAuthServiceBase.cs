using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LandWind.Blog.Application.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace LandWind.Blog.Application.Authorize.OAuth
{
    /// <summary>
    /// OAuth 基类
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    /// <typeparam name="TAccessToken"></typeparam>
    /// <typeparam name="TUserInfo"></typeparam>
    public abstract class OAuthServiceBase<TOptions, TAccessToken, TUserInfo> : IOAuthService<TAccessToken, TUserInfo>, ITransientDependency where TOptions : class where TAccessToken : class where TUserInfo : class
    {
        private IUserService _userService;
        private IHttpClientFactory _httpClient;

        protected readonly object ServiceProviderLock = new object();
        public IServiceProvider ServiceProvider { get; set; }
        public IOptions<TOptions> Options { get; set; }

        protected IUserService UserService => LazyGetRequiredService(ref _userService);
        protected IHttpClientFactory HttpClient => LazyGetRequiredService(ref _httpClient);

        private TService LazyGetRequiredService<TService>(ref TService service)
        {
            return LazyGetRequiredService(typeof(TService), ref service);
        }

        private TService LazyGetRequiredService<TService>(Type type, ref TService service)
        {
            if (service == null)
            {
                lock (ServiceProviderLock)
                {
                    if (service == null)
                    {
                        service = (TService)ServiceProvider.GetRequiredService(type);
                    }
                }
            }

            return service;
        }

        public virtual Task<string> GetAuthorizeUrlAsync(string state) => throw new NotImplementedException();

        public virtual Task<Core.Domain.Users.User> GetUserByOAuthAsync(string type, string code, string state) => throw new NotImplementedException();

        public virtual Task<TAccessToken> GetAccessTokenAsync(string code, string state) => throw new NotImplementedException();

        public virtual Task<TUserInfo> GetUserInfoAsync(TAccessToken accessToken) => throw new NotImplementedException();
    }
}
