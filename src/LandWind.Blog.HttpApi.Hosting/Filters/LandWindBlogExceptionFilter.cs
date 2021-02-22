using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LandWind.Blog.HttpApi.Hosting.Filters
{
    public class LandWindBlogExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            //LoggerHelper.WriteToFile($"{context.HttpContext.Request.Path |{context.Exception.Message}", context.Exception);
        }
    }
}
