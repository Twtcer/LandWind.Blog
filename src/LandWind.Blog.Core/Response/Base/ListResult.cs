using System;
using System.Collections.Generic;
using System.Text; 

namespace LandWind.Blog.Core.Response.Base
{
    public class ListResult<T> : IListResult<T>
    {
        IReadOnlyList<T> items;
        public IReadOnlyList<T> Items 
        { 
            get =>items??(items=new List<T>());
            set => items = value; 
        }

        public ListResult()
        { }

        public ListResult(IReadOnlyList<T> items)
        {
            Items = items;
        }
    }
}
