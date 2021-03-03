using System;
using System.Collections.Generic;
using System.Text;

namespace LandWind.Blog.Core.Response.Base
{
    public interface IListResult<T>
    {
        IReadOnlyList<T> Items { get; set; }
    }
}
