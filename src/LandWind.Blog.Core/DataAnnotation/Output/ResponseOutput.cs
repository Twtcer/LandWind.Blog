using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using AutoMapper;

namespace LandWind.Blog.Core.DataAnnotation.Output
{

    /// <summary>
    /// ResponseOutput
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseOutput<T>:IResponseOutput<T>
    {
        /// <summary>
        /// 响应是否成功
        /// </summary>
        [JsonIgnore]
        public bool Success { get; private set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public int Code => Success ? 1 : 0;

        /// <summary>
        /// 响应消息
        /// </summary>
        public string Msg { get; private set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; private set; }

        public ResponseOutput<T> Ok(T data, string msg = null)
        {
            Success = true;
            Data = data;
            Msg = msg;

            return this;
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public ResponseOutput<T> NotOk(string msg = null, T data = default(T))
        {
            Success = false;
            Msg = msg;
            Data = data;

            return this;
        }

        /// <summary>
        /// 异常
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public ResponseOutput<T> Error(string msg, T exception = default(T))
        {
            Success = false;
            Msg = msg;
            Data = exception;

            return this;
        }
    }

    /// <summary>
    /// 响应数据静态输出
    /// </summary>
    public static partial class ResponseOutput
    {
        /// <summary>
        /// 成功
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static IResponseOutput Ok<T>(T data = default(T), string msg = null)
        {
            return new ResponseOutput<T>().Ok(data, msg);
        }

        public static IResponseOutput Ok<Ts, Tt>(IMapper mapper, Ts data = default(Ts), string msg = null)
        {
            return new ResponseOutput<Tt>().Ok(mapper.Map<Tt>(data), msg);
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <returns></returns>
        public static IResponseOutput Ok()
        {
            return Ok<string>();
        }

        /// <summary>
        /// 失败输出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IResponseOutput NotOk<T>(string msg = null, T data = default(T))
        {
            return new ResponseOutput<T>().NotOk(msg, data);
        }

        /// <summary>
        /// 失败
        /// </summary> 
        /// <param name="msg"></param>
        /// <returns></returns>
        public static IResponseOutput NotOk(string msg = null)
        {
            return new ResponseOutput<string>().NotOk(msg);
        }

        /// <summary>
        /// 错误异常
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static IResponseOutput Error(string msg, Exception exception=null)
        {
            return new ResponseOutput<Exception>().Error(msg,exception);
        }

        /// <summary>
        /// 泛型根据success返回结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="success"></param>
        /// <returns></returns>
        public static IResponseOutput Result<T>(bool success)
        {
            return success ? Ok<T>() : NotOk<T>();
        }

        /// <summary>
        /// 根据success返回结果
        /// </summary>
        /// <param name="success"></param>
        /// <returns></returns>
        public static IResponseOutput Result(bool success)
        {
            return success ? Ok() : NotOk();
        }

        /// <summary>
        /// 返回分页数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static IResponseOutput PageResult<T>(List<T> data, long total)
        {
            var pageOutput = new PageOutput<T>()
            {
                List = data,
                Total = total
            };

            return Ok(pageOutput);
        }

        public static IResponseOutput PageResult<Ts, Tt>(IMapper _mapper, List<Ts> data, long total)
        {
            var pageOutput = new PageOutput<Tt>()
            {
                List = _mapper.Map<List<Tt>>(data),
                Total = total
            };

            return Ok(pageOutput);
        }

    }
}
