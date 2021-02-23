using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.MySql.Core;
using LandWind.Blog.Domain.Configurations;
using Volo.Abp;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.Modularity;

namespace LandWind.Blog.BackgroundJobs
{
    [DependsOn(typeof(AbpBackgroundJobsHangfireModule))]
    public class LandWindBlogBackgroundJobsModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHangfire(options =>
            {
                options.UseStorage(
                    new MySqlStorage(Appsettings.ConnectionStrings, 
                    new MySqlStorageOptions { 
                        TablePrefix = Domain.Shared.LandWindBlogConsts.DbTablePrefix+ "hangfire"
                    }));
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseHangfireServer();
            app.UseHangfireDashboard();
        }
    }
}
