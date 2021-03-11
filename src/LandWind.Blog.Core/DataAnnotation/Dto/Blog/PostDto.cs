using System;
using System.Collections.Generic;
using System.Text;

namespace LandWind.Blog.Core.Dto.Blog
{
    /// <summary>
    /// PostDto
    /// </summary>
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

    /// <summary>
    /// CreatePostInput
    /// </summary>
    public class CreatePostInput : PostDto
    {
        public List<string> Tags { get; set; } = new List<string>();
        public string GeneratePostUrl { get; set; }
    }

    /// <summary>
    /// UpdatePostInput
    /// </summary>
    public class UpdatePostInput: CreatePostInput
    {
         
    }

    /// <summary>
    ///  QueryPostDto
    /// </summary>
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
        public DateTime CreationTime { get; set; }
    }

    /// <summary>
    /// 文章详情
    /// </summary>
    public class PostDetailDto
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string Url { get; set; }

        public string Markdown { get; set; }

        public CategoryDto Category { get; set; }

        public List<TagDto> Tags { get; set; }

        public DateTime CreationTime { get; set; }

        public PostPagedDto Previous { get; set; }

        public PostPagedDto Next { get; set; }
    }

    public class PostPagedDto
    {
        public string Title { get; set; }

        public string Url { get; set; }
    }

}
