using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandWind.Blog.Core.Options.Authorize
{
    public class GithubOptions
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUrl { get; set; }
        public string Scope { get; set; }
        public string AuthorizeUrl { get; set; } = "https://github.com/login/oauth/authorize";
        public string AccessTokenUrl { get; set; } = "https://github.com/login/oauth/access_token";
        public string UserInfoUrl { get; set; } = "https://api.github.com/user";
    }
}
