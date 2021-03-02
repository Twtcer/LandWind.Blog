using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LandWind.Blog.Core.Extensions;
using MimeKit;

namespace LandWind.Blog.BackgroundJobs.Jobs
{
   public class EmailJobTest : IBackgroundJob
    {
        public async Task ExecuteAsync()
        {
            // 发送Email
            var message = new MimeMessage
            {
                Subject = "【定时任务】数据抓取任务推送",
                Body = new BodyBuilder
                {
                    HtmlBody = $"本次抓取到{10}条数据，时间:{DateTime.Now:yyyy-MM-dd HH:mm:ss}"
                }.ToMessageBody()
            };
            await EmailHelper.SendAsync(message);
        }
    }
}
