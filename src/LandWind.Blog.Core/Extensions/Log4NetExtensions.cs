﻿//using System.IO;
//using System.Reflection; 
//using Microsoft.Extensions.Hosting;

//namespace LandWind.Blog.Core.Extensions
//{
//    public static class Log4NetExtensions
//    {
//        public static IHostBuilder UseLog4Net(this IHostBuilder hostBuilder)
//        {
//            var log4netRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
//            XmlConfigurator.Configure(log4netRepository, new FileInfo("log4net.config"));

//            return hostBuilder;
//        }
//    }
//}
