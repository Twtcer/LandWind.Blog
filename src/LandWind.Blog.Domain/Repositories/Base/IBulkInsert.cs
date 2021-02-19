using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandWind.Blog.Domain.Repositories
{
    /// <summary>
    /// 批量插入接口
    /// </summary>
    public interface IBulkInsert<T>
    {
        Task BulkInsertAsync(IEnumerable<T> ts);
    }
}
