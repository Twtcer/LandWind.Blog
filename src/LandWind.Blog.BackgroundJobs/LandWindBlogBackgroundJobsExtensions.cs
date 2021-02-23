using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using LandWind.Blog.BackgroundJobs.Jobs.Hangfire;

namespace LandWind.Blog.BackgroundJobs
{
    public static class LandWindBlogBackgroundJobsExtensions
    {
        public static void UseHangfireTest(this IServiceProvider service)
        {
            var job = service.GetService(typeof(HangfireTestJob));

            //RecurringJob.AddOrUpdate("定时任务测试", () => job.ExecuteAsync(), CronType.Minute());
        }
    }
}
