using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LandWind.Blog.Core.Domain.Options;

namespace LandWind.Blog.Domain.Configurations
{
    /// <summary>
    /// GithubConfig 配置类
    /// </summary>
    public class GithubConfig
    {
        /// <summary>
        ///  get请求，获取用户授权后得到code
        /// </summary>
        public static string ApiAuthorizeUrl = "https://github.com/login/oauth/authorize";

        /// <summary>
        ///  POST请求，根据code得到access_token
        /// </summary>
        public static string ApiAccessTokenUrl { get; set; } = "https://github.com/login/oauth/access_token";

        /// <summary>
        /// GET请求，根据access_token得到用户信息
        /// </summary>
        public static string ApiUserUrl = "https://api.github.com/user";

        /// <summary>
        /// Github UserId
        /// </summary>
        public static int UserId = Appsettings.Github.UserId;

        /// <summary>
        /// Client ID
        /// </summary>
        public static string ClientId = Appsettings.Github.ClientId;

        /// <summary>
        /// Client Secret
        /// </summary>
        public static string ClientSecret = Appsettings.Github.ClientSecret;

        /// <summary>
        /// Authorization callback URL
        /// </summary>
        public static string RedirectUri = Appsettings.Github.RedirectUri;

        /// <summary>
        /// Application name
        /// </summary>
        public static string ApplicationName = Appsettings.Github.ApplicationName;
    }
}
