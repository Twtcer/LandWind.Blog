using System;
using LandWind.Blog.Core.Response.Enum;

namespace LandWind.Blog.Core.Response.Base
{
    /// <summary>
    /// 接口响应
    /// </summary>
    public class ResponseResult
    {
        public static ResponseResult Instance = new ResponseResult();

        /// <summary>
        /// 响应代码
        /// </summary>
        public ResponseResultCode Code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        public bool Success => Code == ResponseResultCode.Succeed;

        /// <summary>
        /// 时间戳
        /// </summary>
        public long Timestamp { get; } = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;

        /// <summary>
        /// 响应成功 
        /// </summary>
        /// <param name="message"></param>
        public void IsSuccess(string message = "")
        {
            Message = message;
            Code = ResponseResultCode.Succeed;
        }

        /// <summary>
        /// 响应失败
        /// </summary>
        /// <param name="message"></param>
        public void IsFailed(string message = "")
        {
            Message = message;
            Code = ResponseResultCode.Failed;
        }

        /// <summary>
        /// 响应失败
        /// </summary>
        /// <param name="exception"></param>
        public void IsFailed(Exception exception)
        {
            Message = exception.InnerException?.StackTrace;
            Code = ResponseResultCode.Failed;
        }

    }

    /// <summary>
    /// 泛型接口响应
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseResult<T> : ResponseResult where T : class
    { 
        public static ResponseResult<T> Instance = new ResponseResult<T>();

        /// <summary>
        /// 返回结果
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// 响应成功
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        public void IsSuccessed(T result = null, string message = "")
        {
            Message = message;
            Code = ResponseResultCode.Succeed;
            Result = result;
        }
    }
}
