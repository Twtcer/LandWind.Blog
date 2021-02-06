using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LandWind.Blog.Swagger
{
    public static class LandWindBlogSwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "1.0.0",
                    Title = "LandWindBlog Interface",
                    Description = "博客接口"
                });
                var baseDir = AppContext.BaseDirectory;
                options.IncludeXmlComments(Path.Combine(baseDir, "LandWind.Blog.Domain.xml"));
                options.IncludeXmlComments(Path.Combine(baseDir, "LandWind.Blog.HttpApi.xml"));
                options.IncludeXmlComments(Path.Combine(baseDir, "LandWind.Blog.Application.Contracts.xml"));
            });
        }

        public static void UseSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/swagger/v1/swagger.json", "apiv1");
            });
        }
    }
}
