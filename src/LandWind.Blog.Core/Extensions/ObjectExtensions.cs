﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
        public static T Deserialize<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 文本UFT8字节编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ToUtf8(this string str)
        {
            return System.Text.Encoding.UTF8.GetBytes(str);
        }

        /// <summary>
        /// url参数化
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string ToQueryString(this Dictionary<string, string> dict)
        {
            return dict.Select(x => $"{x.Key}={x.Value}").JoinAsString("&");
        }

        /// <summary>
        /// 带编码参数化
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static string ToQueryString(this Dictionary<string, string> dict,Encoding encoding)
        { 
            return dict.Select(x => $"{HttpUtility.UrlEncode(x.Key, encoding)}={HttpUtility.UrlEncode(x.Value, encoding)}").JoinAsString("&");
        }

        public static string GenerateRandomCode(this int length)
        {
            int rand;
            char code;
            var randCode = string.Empty;
            var random = new Random();

            for (int i = 0; i < length; i++)
            {
                rand = random.Next();
                if (rand % 3 == 0)
                {
                    code = (char)('A' + (char)(rand % 26));
                }
                else
                {
                    code = (char)('0' + (char)(rand % 10));
                }
                randCode += code.ToString();
            }
            return randCode;
        }

    }
}
