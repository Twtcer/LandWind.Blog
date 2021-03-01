using System;
using Hangfire;
using LandWind.Blog.BackgroundJobs.Jobs;
using LandWind.Blog.BackgroundJobs.Jobs.Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace LandWind.Blog.BackgroundJobs
{
    public static class LandWindBlogBackgroundJobsExtensions
    {
        public static void UseHangfireTest(this IServiceProvider service)
        {
            //var job1 = service.GetService<HangfireTestJob>(); 
            //RecurringJob.AddOrUpdate("定时任务测试", () => job1.ExecuteAsync(), CronType.Minute());

            var job2 = service.GetService<PuppeteerTestJob>();
            RecurringJob.AddOrUpdate("PuppeteerTestJob测试", () => job2.ExecuteAsync(), CronType.Day());
        }
    }
}
