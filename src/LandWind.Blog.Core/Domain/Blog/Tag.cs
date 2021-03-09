using Volo.Abp.Domain.Entities;

namespace LandWind.Blog.Core.Domain.Entities
{
    public class Tag : EntityBase<int>
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 展示名称
        /// </summary>
        public string Alias { get; set; }
    }
}
