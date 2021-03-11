using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks; 

namespace LandWind.Blog.Core.DataAnnotation.Output
{
    /// <summary>
    /// IResponseOutput
    /// </summary>
    public interface IResponseOutput
    {
        /// <summary>
        /// Success
        /// </summary>
        [JsonIgnore]
         bool Success { get;  } 

        /// <summary>
        /// Msg
        /// </summary>
        string Msg { get; }
    }

    /// <summary>
    /// IResponseOutput
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResponseOutput<T> : IResponseOutput
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        T Data { get; }
    }
}
