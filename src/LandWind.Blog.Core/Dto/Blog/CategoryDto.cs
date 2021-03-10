using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandWind.Blog.Core.Dto.Blog
{
    public class CategoryDto
    {
        public string Name { get; set; }

        public string Alias { get; set; }
    }

    public class QueryCategoryDto : CategoryDto
    {
        public int Total { get; set; }
    }

    public class CreateCategoryInput: CategoryDto
    { 
        
    } 

    public class UpdateCategoryInput : CreateCategoryInput
    {
    }
}
