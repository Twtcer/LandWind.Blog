using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandWind.Blog.Core.Options
{
    /// <summary>
    /// SwaggerOptions
    /// </summary>
    public class SwaggerOptions
    {
        public string Version { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RoutePrefix { get; set; } 
        public string DocumentTitle { get; set; }
    }
}
