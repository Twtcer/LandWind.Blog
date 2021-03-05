using System;
using System.Threading.Tasks;
using LandWind.Blog.Core.Response.Base;

namespace LandWind.Blog.Application.Caching.Authorize
{
    public interface IAuthorizeCacheService
    {
        Task AddAuthorizeCodeAsync(string code); 
        Task<string> GetAuthorizeCodeAsync();
    }
}
