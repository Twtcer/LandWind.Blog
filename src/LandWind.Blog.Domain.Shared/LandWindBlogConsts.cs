using System;
using System.Collections.Generic;
using System.Text;

namespace LandWind.Blog.Domain.Shared
{
    /// <summary>
    /// 全局常量
    /// </summary>
    public class LandWindBlogConsts
    {
        public const string DbTablePrefix = "Blog_";
    }

    /// <summary>
    /// 分组
    /// </summary>
    public static class Grouping
    {
        /// <summary>
        /// 博客前台接口
        /// </summary>
        public const string GroupName_v1 = "v1";

        /// <summary>
        /// 博客后台接口
        /// </summary>
        public const string GroupName_v2 = "v2";

        /// <summary>
        /// 其他通用接口
        /// </summary>
        public const string GroupName_v3 = "v3";

        /// <summary>
        /// JWT授权接口
        /// </summary>
        public const string GroupName_v4 = "v4";
    }

    /// <summary>
    /// 缓存过期时间策略
    /// </summary>
    public static class CacheStrategy
    {
        /// <summary>
        /// 1day 24h
        /// </summary>
        public const int OneDay = 1440;

        /// <summary>
        ///  12h 
        /// </summary>
        public const int HalfDay = 720;

        /// <summary>
        /// 8h
        /// </summary>
        public const int EightHours = 480;

        /// <summary>
        /// 4h
        /// </summary>
        public const int FourHours = 240;

        /// <summary>
        /// 3小时过期
        /// </summary>

        public const int ThreeHours = 180;

        /// <summary>
        /// 2小时过期
        /// </summary>

        public const int TwoHours = 120;

        /// <summary>
        /// 1小时过期
        /// </summary>

        public const int OneHours = 60;

        /// <summary>
        /// 半小时过期
        /// </summary>

        public const int HalfHours = 30;

        /// <summary>
        /// 5分钟过期
        /// </summary>
        public const int FiveMinute = 5;

        /// <summary>
        /// 1分钟过期
        /// </summary>
        public const int OneMinute = 1;

        /// <summary>
        /// 永不过期
        /// </summary>

        public const int Never = -1;

    }
}
