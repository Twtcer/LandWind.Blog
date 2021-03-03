using Volo.Abp.Domain.Entities;

namespace LandWind.Blog.Core.Domain.Entities
{
    public class FriendLink : EntityBase<int>
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string LinkUrl { get; set; }
    }
}
