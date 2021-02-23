using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LandWind.Blog.Core.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// 序列号json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T DeserializeToObject<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static byte[] ToUtf8(this string str)
        {
            return System.Text.Encoding.UTF8.GetBytes(str);
        }
    }
}
