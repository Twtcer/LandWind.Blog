using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace LandWind.Blog.Application.Authorize.OAuth
{
    /// <summary>
    /// 授权接口
    /// </summary>
    /// <typeparam name="TAccessToken"></typeparam>
    /// <typeparam name="TUserInfo"></typeparam>
    public interface IOAuthService<TAccessToken,TUserInfo>
    {
        Task<string> GetAuthorizeUrlAsync(string state);
        Task<Core.Domain.Users.User> GetUserByOAuthAsync(string type, string code, string state);
        Task<TAccessToken> GetAccessTokenAsync(string code, string state);
        Task<TUserInfo> GetUserInfoAsync(TAccessToken accessToken); 
    }
}
