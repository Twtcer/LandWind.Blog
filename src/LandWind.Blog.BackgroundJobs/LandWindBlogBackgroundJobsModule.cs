using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.MySql; 
using LandWind.Blog.Core.Domain.Options;
using LandWind.Blog.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.Modularity;

namespace LandWind.Blog.BackgroundJobs
{
    [DependsOn(
        typeof(AbpBackgroundJobsHangfireModule)
        )]
    public class LandWindBlogBackgroundJobsModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHangfire(options =>
            {
                options.UseStorage(
                    new MySqlStorage(Appsettings.ConnectionStrings, 
                    new MySqlStorageOptions {
                        TablesPrefix = LandWindBlogDbConsts.DbTablePrefix+ "hangfire"
                    }));
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseHangfireServer();
            //启用Hangfire基本授权验证
            app.UseHangfireDashboard(options: new DashboardOptions
            {
                Authorization = new[] {
                    new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions{
                        RequireSsl = false,
                        SslRedirect  = false,
                        LoginCaseSensitive = true,
                        Users = new []
                        {
                            new BasicAuthAuthorizationUser{
                                Login = Appsettings.Hangfire.Login,
                                PasswordClear = Appsettings.Hangfire.Password
                            }
                        }
                    })
                },
                DashboardTitle="任务调度管理"
            }) ;

            var service = context.ServiceProvider;
            service.AddHangfireJobs();

        }
    }
}
