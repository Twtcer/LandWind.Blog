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
    }
}
