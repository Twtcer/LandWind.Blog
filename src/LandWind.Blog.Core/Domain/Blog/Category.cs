
namespace LandWind.Blog.Core.Domain.Entities
{
    public class Category : EntityBase<int>
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; }
    }
}
 