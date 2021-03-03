using System;
using System.Collections.Generic;
using System.IO; 
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace LandWind.Blog.Swagger
{
    public static class LandWindBlogSwaggerExtensions
    { 
        private static readonly List<SwaggerApiInfo> ApiInfos = new List<SwaggerApiInfo> {
            new SwaggerApiInfo{
                UrlPrefix = Grouping.GroupName_v1,
                Name = "博客前台接口",
                OpenApiInfo  = new OpenApiInfo
                {
                    Version = Appsettings.ApiVersion,
                    Title = "博客前台接口",
                    Description = ""
                }
            } ,
            new SwaggerApiInfo
            {
                UrlPrefix = Grouping.GroupName_v2,
                Name = "博客后台接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version =  Appsettings.ApiVersion,
                    Title = "博客后台接口",
                    Description = ""
                }
            },
            new SwaggerApiInfo
            {
                UrlPrefix = Grouping.GroupName_v3,
                Name = "通用公共接口",
                OpenApiInfo = new OpenApiInfo
                {
                                 Version =  Appsettings.ApiVersion,
                    Title = "通用公共接口",
                    Description = ""
                }
            },
            new SwaggerApiInfo
            {
                UrlPrefix = Grouping.GroupName_v4,
                Name = "JWT授权接口",
                OpenApiInfo = new OpenApiInfo
                {
                        Version =  Appsettings.ApiVersion,
                    Title = "JWT授权接口",
                    Description = ""
                }
            }
        };
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            { 
                ApiInfos.ForEach(api =>
                {
                    options.SwaggerDoc(api.UrlPrefix, api.OpenApiInfo);
                });

                var security = new OpenApiSecurityScheme
                {
                    Description = "JWT模式授权，请输入 Bearer {Token} 进行身份验证",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                };
                options.AddSecurityDefinition("oauth2", security);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement { { security, new List<string>() } });
                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>();

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
                ApiInfos.ForEach(api =>
                {
                    options.SwaggerEndpoint($"/swagger/{api.UrlPrefix}/swagger.json", api.Name);
                });

                //模型扩展深度，设置-1完全隐藏模型
                options.DefaultModelsExpandDepth(-1);
                //API文档仅展开标记
                options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
                //API页面title
                options.DocumentTitle = "博客Plus接口文档";
                // API前缀设置为空
                options.RoutePrefix = string.Empty;
            });
        }

        internal class SwaggerApiInfo
        {
            /// <summary>
            /// URL前缀
            /// </summary>
            public string UrlPrefix { get; set; }

            /// <summary>
            /// 名称
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// <see cref="Microsoft.OpenApi.Models.OpenApiInfo"/>
            /// </summary>
            public OpenApiInfo OpenApiInfo { get; set; }
        }

    }
}
