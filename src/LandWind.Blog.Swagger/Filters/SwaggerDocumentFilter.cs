using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LandWind.Blog.Swagger.Filters
{
    /// <summary>
    /// Controller 扩展信息
    /// </summary>
    public class SwaggerDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var tags = new List<OpenApiTag>
            {
                new OpenApiTag{
                    Name = "Blog",
                    Description = "个人博客相关接口",
                    ExternalDocs = new OpenApiExternalDocs{ Description = "包含:文章/标签/分类/友链"}
                },
                new OpenApiTag
                {
                    Name = "HelloWorld",
                    Description = "通用公共接口",
                    ExternalDocs = new OpenApiExternalDocs{ Description = "通用公共接口"}
                }
            };
            swaggerDoc.Tags = tags.OrderBy(a => a.Name).ToList();
        }
    }
}
