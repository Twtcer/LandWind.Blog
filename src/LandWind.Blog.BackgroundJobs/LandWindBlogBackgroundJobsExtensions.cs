using System;
using Hangfire;
using LandWind.Blog.BackgroundJobs.Jobs; 
using Microsoft.Extensions.DependencyInjection;

namespace LandWind.Blog.BackgroundJobs
{
    public static class LandWindBlogBackgroundJobsExtensions
    {
        public static void AddHangfireJobs(this IServiceProvider service)
        { 
            var job2 = service.GetService<PuppeteerTestJob>();
            RecurringJob.AddOrUpdate("PuppeteerTestJob测试", () => job2.ExecuteAsync(), CronType.Day());

            var job3 = service.GetService<HotNewsJob>();
            RecurringJob.AddOrUpdate("热点新闻抓取", () => job3.ExecuteAsync(), CronType.Hour(30));
            
        }
    }
}
