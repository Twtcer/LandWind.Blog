using System;
using System.Collections.Generic;
using System.Text;

namespace LandWind.Blog.Application.Contracts.Blog
{
    public class PostDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// HTML
        /// </summary>
        public string Html { get; set; }

        /// <summary>
        /// Markdown
        /// </summary>
        public string Markdown { get; set; }

        /// <summary>
        /// 分类Id
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }

    public class QueryPostDto
    {
        public int Year { get; set; }
        public IEnumerable<PostBriefDto> Posts { get; set; }
    }

    /// <summary>
    /// 文章模型
    /// </summary>
    public class PostBriefDto
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public int Year { get; set; }
        public string CreationTime { get; set; }
    }
}
