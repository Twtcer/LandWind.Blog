using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace LandWind.Blog.Domain.Configurations
{
    /// <summary>
    /// appsettings.jso配置类
    /// </summary>
    public class Appsettings
    {
        private static readonly IConfigurationRoot _config;

        static Appsettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);
            _config = builder.Build();
        }

        public static string EnableDb => _config["ConnectionStrings:Enable"];

        public static string ConnectionStrings => _config.GetConnectionString(EnableDb);

        public static string ApiVersion => _config["App:ApiVersion"];

        public static class Github
        {
            public static int UserId => Convert.ToInt32(_config["Github:UserId"]);
            public static string ClientId => _config["Github:ClientId"];
            public static string ClientSecret => _config["Github:ClientSecret"];
            public static string RedirectUri => _config["Github:RedirectUri"];
            public static string ApplicationName => _config["Github:ApplicationName"];
        }

        public static class JWT
        {
            public static string Domain => _config["JWT:Domain"];

            public static string SecurityKey => _config["JWT:SecurityKey"];

            public static int Expires => Convert.ToInt32(_config["JWT:Expires"]);
        }

    }

}
