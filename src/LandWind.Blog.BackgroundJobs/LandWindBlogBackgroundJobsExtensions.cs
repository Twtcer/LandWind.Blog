﻿using System;
using Hangfire;
using LandWind.Blog.BackgroundJobs.Jobs.Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace LandWind.Blog.BackgroundJobs
{
    public static class LandWindBlogBackgroundJobsExtensions
    {
        public static void UseHangfireTest(this IServiceProvider service)
        {
            var job = service.GetService<HangfireTestJob>();

            RecurringJob.AddOrUpdate("定时任务测试", () => job.ExecuteAsync(), CronType.Minute());
        }
    }
}
