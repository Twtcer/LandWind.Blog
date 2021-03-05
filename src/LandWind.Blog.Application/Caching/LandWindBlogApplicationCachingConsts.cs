using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandWind.Blog.Application.Caching
{
    public static class ApplicationCachingConsts
    {
        public class CachePrefix
        {
            public static string Blog = "Blog";
            public static string BlogPost = $"{Blog}:Post";
            public static string BlogCategory = $"{Blog}:Category";
            public static string BlogTag = $"{Blog}:Tag";
            public static string BlogFriendLink = $"{Blog}:FriendLink";

            public const string BlogHotNews= "HotNews";
            public const string Signature = "Signature";
            public static string Authorize = "Authorize:Code";
        }

        /// <summary>
        /// CacheKeys
        /// </summary>
        public class CacheKeys
        {
            public static string GetPostByUrl(string url) => $"{CachePrefix.BlogPost}:GetByUrl-{url}";

            public static string GetPosts(int page, int limit) => $"{CachePrefix.BlogPost}:Get-{page}-{limit}";

            public static string GetPostsByCategory(string category) => $"{CachePrefix.BlogPost}:GetByCategory-{category}";

            public static string GetPostsByTag(string tag) => $"{CachePrefix.BlogPost}:GetByTag-{tag}";

            public static string GetCategories() => $"{CachePrefix.BlogCategory}:Get";

            public static string GetTags() => $"{CachePrefix.BlogTag}:Get";

            public static string GetFriendLinks() => $"{CachePrefix.BlogFriendLink}:Get";

            public static string GetSources() => $"{CachePrefix.BlogHotNews}:Sources";

            public static string GetHots(string source) => $"{CachePrefix.BlogHotNews}:{source}";

            public static string GetSignatureTypes() => $"{CachePrefix.Signature}:Types";

            public static string GenerateSignature(string name, int typeId) => $"{CachePrefix.Signature}:{name}-{typeId}";
        }

        /// <summary>
        /// 缓存过期时间策略
        /// </summary>
        public class CacheStrategy
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
}
