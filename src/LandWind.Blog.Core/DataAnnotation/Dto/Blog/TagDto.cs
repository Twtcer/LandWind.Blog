using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandWind.Blog.Core.Dto.Blog
{
    public class TagDto
    {
        public string Name { get; set; }

        public string Alias { get; set; }
    }

    public class QueryTagDto : TagDto
    {
        public int Total { get; set; }
    }

    public class CreateTagInput: TagDto
    {

    }

    public class UpdateTagInput : CreateTagInput
    {

    }

}
