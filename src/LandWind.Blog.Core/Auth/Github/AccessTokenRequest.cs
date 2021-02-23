using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LandWind.Blog.Domain.Configurations;

namespace LandWind.Blog.Core.Auth.Github
{
   public class AccessTokenRequest
    {
        /// <summary>
        /// Client ID
        /// </summary>
        public string ClientId = GithubConfig.ClientId;

        /// <summary>
        /// Client Secret
        /// </summary>
        public string ClientSecret = GithubConfig.ClientSecret;

        /// <summary>
        /// 调用API_Authorize获取到的Code值
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Authorization callback URL
        /// </summary>
        public string RedirectUri = GithubConfig.RedirectUri;

        /// <summary>
        /// State
        /// </summary>
        public string State { get; set; }
    }
}
