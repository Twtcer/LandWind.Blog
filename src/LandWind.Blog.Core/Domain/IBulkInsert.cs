using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandWind.Blog.Core.Domain
{
    /// <summary>
    /// 批量插入接口
    /// </summary>
    public interface IBulkInsert<T>
    {
        Task BulkInsertAsync(IEnumerable<T> ts);
    }
}
