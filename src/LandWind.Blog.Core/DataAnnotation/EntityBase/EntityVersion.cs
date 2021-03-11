using System.ComponentModel;
using FreeSql.DataAnnotations;

namespace LandWind.Blog.Core.EntityBase
{
    public interface  IEntityVersion
    {
        /// <summary>
        /// 版本
        /// </summary>
        long Version { get; set; }
    }

    public class EntityVersion<TKey> : Entity<TKey>, IEntityVersion
    {
        /// <summary>
        /// 版本
        /// </summary>
        [Description("版本")]
        [Column(Position = -1, IsVersion = true)]
        public long Version { get; set; }
    }
    public class EntityVersion : EntityVersion<int>
    {
    }
}
