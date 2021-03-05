using System;
using System.Net;
using System.Threading.Tasks;
using LandWind.Blog.Core.Extensions;
using LandWind.Blog.Core.Response.Base;
using Microsoft.AspNetCore.Http;

namespace LandWind.Blog.HttpApi.Hosting.Middleware
{
    /// <summary>
    /// 异常处理中间件
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        public ExceptionHandlerMiddleware(RequestDelegate request)
        {
            this.next = request;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await ExceptionHandlerAsync(context, ex.Message);
            }
            finally
            {
                var statusCode = context.Response.StatusCode;
                if (statusCode != StatusCodes.Status200OK)
                {
                    Enum.TryParse(typeof(HttpStatusCode), statusCode.ToString(), out object message);
                    await ExceptionHandlerAsync(context, message.ToString());
                }
            }
        }

        /// <summary>
        /// 异常处理，返回JSON
        /// </summary>
        /// <param name="context"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task ExceptionHandlerAsync(HttpContext context, string message)
        {
            context.Response.ContentType = "application/json;charset=utf-8";

            var result = new ResponseResult();
            result.IsFailed(message);

            await context.Response.WriteAsync(result.SerializeToJson());
        }
    }
}
