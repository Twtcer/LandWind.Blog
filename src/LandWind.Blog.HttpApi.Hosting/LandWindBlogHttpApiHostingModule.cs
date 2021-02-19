using System;
using LandWind.Blog.Domain.Configurations;
using LandWind.Blog.EntityFrameworkCore;
using LandWind.Blog.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace LandWind.Blog.HttpApi.Hosting
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreMvcModule),
        typeof(LandWindBlogHttpApiModule),
        typeof(LandWindBlogSwaggerModule),
        typeof(LandWindBlogEFCoreModule)
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
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
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

            //路由
            app.UseRouting();
            app.UseEndpoints(e =>
            {
                e.MapControllers();
            });

            //身份认证
            app.UseAuthentication();

            //授权
            app.UseAuthorization();

        }
    }
}
