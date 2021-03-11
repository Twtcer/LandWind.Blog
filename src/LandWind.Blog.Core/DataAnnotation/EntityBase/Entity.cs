using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace LandWind.Blog.Core.EntityBase
{
    /// <summary>
    /// 实体接口
    /// </summary>
    public interface IEntity
    {
    }

    /// <summary>
    /// Entity TKey
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class Entity<TKey> : IEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        [Description("编号")]
        [Column]
        public virtual TKey Id { get; set; }
    }

    /// <summary>
    /// Entity,Key=int
    /// </summary>
    public class Entity : Entity<int>
    {
        
    }
}
