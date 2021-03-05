﻿using System;
using System.Linq;
using LandWind.Blog.BackgroundJobs;
using LandWind.Blog.Core.Domain.Options;
using LandWind.Blog.HttpApi.Hosting.Filters;
using LandWind.Blog.HttpApi.Hosting.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.ExceptionHandling;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace LandWind.Blog.HttpApi.Hosting
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreMvcModule),
        typeof(LandWindBlogHttpApiModule), 
        typeof(LandWindBlogBackgroundJobsModule)
        )]
    public class LandWindBlogHttpApiHostingModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);

            //添加身份认证
            context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.FromSeconds(300),
                        ValidAudience = Appsettings.JWT.Domain,
                        ValidIssuer = Appsettings.JWT.Domain,
                        IssuerSigningKey = new SymmetricSecurityKey(Appsettings.JWT.SecurityKey.GetBytes())
                    };
                });

            //认证授权
            context.Services.AddAuthorization();

            //http请求
            context.Services.AddHttpClient();

            //配置
            Configure<MvcOptions>(options =>
            {
                var filterMetadata = options.Filters.FirstOrDefault(x => x is ServiceFilterAttribute attribute && attribute.ServiceType.Equals(typeof(AbpExceptionFilter)));

                // 移除 AbpExceptionFilter
                options.Filters.Remove(filterMetadata);

                //添加自定义异常filter
                options.Filters.Add(typeof(LandWindBlogExceptionFilter));
            });

            ////定时任务
            //context.Services.AddTransient<IHostedService, HelloWorldJob>();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                //启用生成异常页面
                app.UseDeveloperExceptionPage();
            }
            //身份认证
            app.UseAuthentication();
            //路由
            app.UseRouting();
            //授权
            app.UseAuthorization();
            app.UseEndpoints(e =>
            {
                e.MapControllers();
            });

            //异常中间件
            app.UseMiddleware<ExceptionHandlerMiddleware>(); 
        }
    }
}
