using System;
using System.Collections.Generic;
using System.Text;
using LandWind.Blog.Domain.Shared.Base.Paged;

namespace LandWind.Blog.Domain.Shared.Base
{
    public class PagedList<T> : ListResult<T>, IPagedList<T>
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }

        public PagedList()
        {
            
        }

        public PagedList(int total,IReadOnlyList<T> result):base(result)
        {
            Total = Items.Count;
        }
    }
}
