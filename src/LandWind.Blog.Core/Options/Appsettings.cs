using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace LandWind.Blog.Core.Domain.Options
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

        public static class Caching
        {
            public static string RedisConnectionString => _config["Caching:RedisConnectionString"];
        }

        public static class Hangfire
        {
            public static string Login => _config["Hangfire:Login"];
            public static string Password => _config["Hangfire:Password"];
        }

        public static class Email
        {
            /// <summary>
            /// Host Address
            /// </summary>
            public static string Host { get; set; } = _config["Email:Host"];

            /// <summary>
            /// Port
            /// </summary>
            public static int Port { get; set; } = Convert.ToInt32(_config["Email:Port"]);

            /// <summary>
            /// UseSsl
            /// </summary>
            public static bool UseSsl { get; set; } = Convert.ToBoolean(_config["Email:UseSsl"]);

            /// <summary>
            /// Form
            /// </summary>
            public static class From
            { 
                /// <summary>
                /// Username
                /// </summary>
                public static string Username => _config["Email:From:Username"];

                /// <summary>
                /// Password
                /// </summary>
                public static string Password => _config["Email:From:Password"];

                /// <summary>
                /// Name
                /// </summary>
                public static string Name => _config["Email:From:Name"]; 
            }

            /// <summary>
            /// To
            /// </summary>
            public static IDictionary<string, string> To
            {
                get
                {
                    var dic = new Dictionary<string, string>();

                    var emails = _config.GetSection("Email:To");
                    foreach (IConfigurationSection section in emails.GetChildren())
                    {
                        var name = section["Name"];
                        var address = section["Address"];

                        dic.Add(name, address);
                    }
                    return dic;
                }
            }
        }
    }
}
