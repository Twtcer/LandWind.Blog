using System;
using System.Text;
using LandWind.Blog.Core;
using LandWind.Blog.Core.Options;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.Modularity;

namespace LandWind.Blog.BackgroundWorkers
{
    [DependsOn(
    typeof(AbpBackgroundWorkersQuartzModule),
    typeof(LandWindBlogCoreModule)
)]
    public class LandWindBlogBackgroundWorksModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var option = context.Services.ExecutePreConfiguredActions<WorkerOptions>();

            Configure<AbpBackgroundWorkerQuartzOptions>(options =>
            {
                options.IsAutoRegisterEnabled = option.IsEnabled;
            });

            context.Services.AddHttpClient();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
    }
}
