using LandWind.Blog.Core.Extensions;
using LandWind.Blog.Core.Response.Base;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LandWind.Blog.Api.Filters
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
            //_log.Error($"{context.HttpContext.Request.Path}|{context.Exception.Message}", context.Exception);
            if (context.Exception != null)
            {
                var result = new ResponseResult();
                result.IsFailed(context.Exception.Message);

                context.Result = new ContentResult()
                {
                    Content = result.SerializeToJson(),
                    StatusCode = StatusCodes.Status200OK
                };

                context.ExceptionHandled = true;
            }
        }
    }
}
