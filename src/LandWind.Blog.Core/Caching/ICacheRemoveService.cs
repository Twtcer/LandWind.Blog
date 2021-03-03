using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandWind.Blog.Core.Caching
{
    public interface ICacheRemoveService
    {
        Task RemoveAsync(string key);
    }
}
