using System;
using System.Collections.Generic;
using System.Text;

namespace LandWind.Blog.Domain.Shared.Base.Paged
{
    public interface IPagedList<T>:IListResult<T>,ITotalCount
    {
    }
}
