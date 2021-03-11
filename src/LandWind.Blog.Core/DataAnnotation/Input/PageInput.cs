using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandWind.Blog.Core.DataAnnotation.Input
{
    /// <summary>
    /// PageInput
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageInput<T>
    {
        /// <summary>
        ///  当前页
        /// </summary>
        public int CurrentPage { get; set; } = 1;

        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; } = 20;

        /// <summary>
        /// 筛选-查询条件
        /// </summary>
        public T Filter{ get; set; }

        /// <summary>
        /// 高级查询条件
        /// </summary>
        public FreeSql.Internal.Model.DynamicFilterInfo DynamicFilter { get; set; }
    }
}
