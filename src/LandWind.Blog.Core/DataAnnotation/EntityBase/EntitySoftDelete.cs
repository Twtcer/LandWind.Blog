using System.ComponentModel;
using FreeSql.DataAnnotations;

namespace LandWind.Blog.Core.EntityBase
{
    /// <summary>
    /// 
    /// </summary>
    public  interface IEntitySoftDelete
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        bool IsDeleted { get; set; }
    }

    public class EntitySoftDelete<TKey> : Entity<TKey>, IEntitySoftDelete
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        [Description("是否删除")]
        [Column(Position = -1)]
        public bool IsDeleted { get; set; } = false;
    }

    public class EntitySoftDelete : EntitySoftDelete<int>
    {
    }
}
