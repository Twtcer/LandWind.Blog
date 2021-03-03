using Volo.Abp.Domain.Entities;

namespace LandWind.Blog.Core.Domain.Entities
{
    public class PostTag : EntityBase<int>
    {
        /// <summary>
        /// 文章Id
        /// </summary>
        public int PostId { get; set; }

        /// <summary>
        /// 标签Id
        /// </summary>
        public int TagId { get; set; }
    }
}
