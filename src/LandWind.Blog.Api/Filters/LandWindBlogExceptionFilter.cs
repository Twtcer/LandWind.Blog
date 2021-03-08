using LandWind.Blog.Core.Extensions;
using LandWind.Blog.Core.Response.Base; 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using Serilog.Events;

namespace LandWind.Blog.Api.Filters
{
    public class LandWindBlogExceptionFilter : IExceptionFilter
    { 
        public LandWindBlogExceptionFilter()
        { 
        } 

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            Log.Logger = new LoggerConfiguration().CreateLogger();
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

                Log.Error(context.Exception, $"{context.HttpContext.Request.Path}|{context.Exception.Message}");
            }
        }
    }
}
