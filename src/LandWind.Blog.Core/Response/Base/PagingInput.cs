using System;
using System.ComponentModel.DataAnnotations;

namespace LandWind.Blog.Core.Response.Base
{
    /// <summary>
    /// 分页参数
    /// </summary>
    public class PagingInput
    {
        /// <summary>
        /// 页码
        /// </summary>
        [Range(1,int.MaxValue)]
        public int Page { get; set; } = 1;

        /// <summary>
        /// 单页数量
        /// </summary>
        [Range(10, 50)]
        public int Limit { get; set; } = 10;
    }
}
