using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandWind.Blog.Core.Const
{
    public class LandWindBlogDbConsts
    {
        public const string DbTablePrefix = "Blog_";
    }

    public static class DbTableName
    {
        public const string Posts = "Posts";

        public const string Categories = "Categories";

        public const string Tags = "Tags";

        public const string PostTags = "PostTags";

        public const string Friendlinks = "Friendlinks";

        public const string HotNews = "HotNews";
    }
}
