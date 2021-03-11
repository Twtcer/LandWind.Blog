using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandWind.Blog.Core.DataAnnotation.Output
{
    /// <summary>
    /// PageOutput
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageOutput<T>
    {
        /// <summary>
        /// Total
        /// </summary>
        public long Total { get; set; } = 0;

        /// <summary>
        /// List
        /// </summary>
        public IList<T> List { get; set; }
    }
}
