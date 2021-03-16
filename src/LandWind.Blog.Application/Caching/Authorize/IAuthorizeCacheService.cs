using System;
using System.Threading.Tasks;

namespace LandWind.Blog.Application.Caching.Authorize
{
    public interface IAuthorizeCacheService
    {
        Task AddAuthorizeCodeAsync(string code); 
        Task<string> GetAuthorizeCodeAsync();
    }
}
