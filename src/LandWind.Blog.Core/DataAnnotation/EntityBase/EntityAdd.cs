using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace LandWind.Blog.Core.EntityBase
{
    /// <summary>
    /// IEntityAdd
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntityAdd<TKey> where TKey : struct
    {
        /// <summary>
        /// 创建者Id
        /// </summary>
        TKey? CreatorId { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        string CreatorName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime? CreationTime { get; set; }
    }

    /// <summary>
    /// EntityAdd
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class EntityAdd<TKey> : Entity<TKey>, IEntityAdd<TKey> where TKey : struct
    {
        /// <summary>
        /// 创建者Id
        /// </summary>
        [Description("创建者Id")]
        [Column]
        public TKey? CreatorId { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        [Description("创建者")]
        [Column]
        public string CreatorName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Description("创建时间")]
        [Column]
        public DateTime? CreationTime { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class EntityAdd : EntityAdd<int>
    {

    }
}
