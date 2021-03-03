﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandWind.Blog.Domain.Shared.Base
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
