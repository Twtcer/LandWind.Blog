using LandWind.Blog.EntityFrameworkCore;
using LandWind.Blog.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace LandWind.Blog.HttpApi.Hosting
{
    [DependsOn(
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpAutofacModule),
        typeof(LandWindBlogHttpApiModule),
        typeof(LandWindBlogSwaggerModule),
        typeof(LandWindBlogEFCoreModule)
        )]
    public class LandWindBlogHttpApiHostingModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);
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

        }
    }
}
