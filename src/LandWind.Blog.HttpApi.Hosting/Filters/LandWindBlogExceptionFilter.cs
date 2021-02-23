using log4net;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LandWind.Blog.HttpApi.Hosting.Filters
{
    public class LandWindBlogExceptionFilter : IExceptionFilter
    {
        private readonly ILog _log;

        public LandWindBlogExceptionFilter()
        {
            _log = LogManager.GetLogger(typeof(LandWindBlogExceptionFilter));
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            _log.Error($"{context.HttpContext.Request.Path}|{context.Exception.Message}", context.Exception);
        }
    }
}
