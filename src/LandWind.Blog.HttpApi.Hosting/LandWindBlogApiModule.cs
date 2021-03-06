using System;
using LandWind.Blog.BackgroundJobs;
using LandWind.Blog.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Modularity;
using Volo.Abp.AspNetCore.Mvc;
using LandWind.Blog.Core.Options;
using Microsoft.AspNetCore.Http;
using System.Linq;
using LandWind.Blog.Core.Response.Base;
using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.ExceptionHandling;
using LandWind.Blog.Api.Filters;
using Volo.Abp.Data;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using System.IO;
using LandWind.Blog.Api.Swagger.Filters;
using Swashbuckle.AspNetCore.Filters;

namespace LandWind.Blog.Api
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpCachingStackExchangeRedisModule),
        typeof(LandWindBlogApplicationModule),
        typeof(LandWindBlogEfCoreDbModule),
        typeof(LandWindBlogBackgroundJobsModule)
        )]
    public class LandWindBlogApiModule : AbpModule
    {
        public AppOptions AppOptions;

        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            AppOptions = context.Services.ExecutePreConfiguredActions<AppOptions>();

            ConfigureExceptionFilter();
            ConfigureAutoApiControllers();
            ConfigureDbConnection();
            ConfigureAutoValidate();
            ConfigureRouting(context.Services);
            ConfigureRedis(context.Services);
            ConfigureCors(context.Services);
            CofiggureHealthChecks(context.Services);
            ConfigureAuthentication(context.Services);
            ConfigureSwaggerServices(context.Services);
        }

        private void ConfigureAutoApiControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(LandWindBlogApplicationModule).Assembly);
            });
        }

        private void ConfigureSwaggerServices(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(AppOptions.Swagger.Name, new OpenApiInfo
                {
                    Title = AppOptions.Swagger.Title,
                    Version = AppOptions.Swagger.Version,
                    Description = AppOptions.Swagger.Description
                });
                options.DocInclusionPredicate((docName, description) => true);
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "LandWind.Blog.Core.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "LandWind.Blog.Application.xml"));
                options.CustomSchemaIds(type => type.FullName);
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Scheme = "bearer",
                    Description = "Enter <code>token</code> for authorization.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.DocumentFilter<LandWindBlogSwaggerDocumentFilter>();
            });
        }

        private void CofiggureHealthChecks(IServiceCollection services)
        {
            //services.AddHealthChecks()
            //        .AddMongoDb(AppOptions.Storage.Mongodb, name: "MongoDB", timeout: TimeSpan.FromSeconds(3))
            //        .AddRedis(AppOptions.Storage.Redis, name: "Redis", timeout: TimeSpan.FromSeconds(3));
        }

        /// <summary>
        /// 配置跨域 
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(AppOptions.Cors.PolicyName, builder =>
                {
                    builder.WithOrigins(AppOptions.Cors.Origins.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(x => x.RemovePostFix("/")).ToArray())
                   .WithExposedHeaders()
                   .SetIsOriginAllowedToAllowWildcardSubdomains()
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
                });
            });
        }

        /// <summary>
        /// 配置redis
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureRedis(IServiceCollection services)
        {
            if (AppOptions.Storage.RedisStatus)
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = AppOptions.Storage.RedisConnection;
                });
            }
        }

        /// <summary>
        /// 配置路由
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureRouting(IServiceCollection services)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.AppendTrailingSlash = true;
            });
        }

        /// <summary>
        /// 配置防伪系统设置
        /// </summary>
        private void ConfigureAutoValidate()
        {
            Configure<AbpAntiForgeryOptions>(options =>
            {
                options.AutoValidate = false;
            });
        }

        /// <summary>
        /// 认证配置
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureAuthentication(IServiceCollection services)
        {
            //添加身份认证
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        ValidIssuer = AppOptions.Jwt.Issuer,
                        ValidAudience = AppOptions.Jwt.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(AppOptions.Jwt.SigningKey.GetBytes())
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = async context =>
                        {
                            context.HandleResponse();
                            context.Response.ContentType = "application/json;charset=utf-8";
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                            var response = new ResponseResult();
                            response.IsFailed("Unauthorized");

                            await context.Response.WriteAsJsonAsync(response);
                        },
                        OnMessageReceived = async context =>
                        {
                            context.Token = context.Request.Query["Token"];
                            await Task.CompletedTask;
                        }
                    };
                });
            services.AddAuthorization();
        }

        /// <summary>
        /// 配置数据库连接
        /// </summary>
        private void ConfigureDbConnection()
        {
            Configure<AbpDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = AppOptions.Storage.ConnectionString;
            });
        }

        /// <summary>
        /// 配置异常筛选器
        /// </summary>
        private void ConfigureExceptionFilter()
        {
            Configure<MvcOptions>(options =>
            {
                var filterMetedata = options.Filters.FirstOrDefault(x => x is ServiceFilterAttribute attribute &&
                    attribute.ServiceType.Equals(typeof(AbpExceptionFilter)));
                options.Filters.Remove(filterMetedata);
                options.Filters.Add(typeof(LandWindBlogExceptionFilter));
            });
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="context"></param>
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                //启用开发异常页面
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto,
                ForwardLimit = null
            });

            app.UseHealthChecks("/api/landwind/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
            {
                ResponseWriter = (context, healthReport) =>
                {
                    context.Response.ContentType = "application/json;charset=utf-8";
                    context.Response.StatusCode = StatusCodes.Status200OK;

                    var result = healthReport.Entries.Select(a => new NameValue
                    {
                        Name = a.Key,
                        Value = a.Value.Status.ToString()
                    });
                    var response = new ResponseResult<IEnumerable<NameValue>>();

                    return context.Response.WriteAsJsonAsync(response);
                }
            });

            app.UseHsts();
            //路由
            app.UseRouting();
            app.UseCors(AppOptions.Cors.PolicyName);
            //认证
            app.UseAuthentication();
            //授权
            app.UseAuthorization();

            //Swagger配置
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.HeadContent = @"";
                options.SwaggerEndpoint($"/swagger/{AppOptions.Swagger.Name}/swagger.json", AppOptions.Swagger.Title);
                options.DefaultModelsExpandDepth(-1);//默认不展开节点
                options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                options.RoutePrefix = AppOptions.Swagger.RouterPrefix;
                options.DocumentTitle = AppOptions.Swagger.DocumentTitle;
            });

            app.UseAuditing();
            app.UseConfiguredEndpoints();
        }
    }
}
