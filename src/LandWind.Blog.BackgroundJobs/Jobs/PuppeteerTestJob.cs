using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace LandWind.Blog.BackgroundJobs.Jobs
{
    public class PuppeteerTestJob : IBackgroundJob
    {
        public async Task ExecuteAsync()
        {
            var chromeVersion = 856583;
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);

            using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                Args = new string[] { "--no-sandbox" }//linux下root用户需要--no-sandbox参数
            });

            using var page = await browser.NewPageAsync();

            await page.SetViewportAsync(new ViewPortOptions
            {
                Width = 1920,
                Height = 1080
            });

            var url = "https://www.cnblogs.com/zhaotianff/p/13528507.html";
            await page.GoToAsync(url, WaitUntilNavigation.Networkidle0);

            var content = await page.GetContentAsync();

            Console.WriteLine("===========抓取页面内容开始==========");
            Console.WriteLine(content);
            Console.WriteLine("===========抓取页面内容结束==========");

            //var screenshotOptions = new ScreenshotOptions
            //{
            //    FullPage = true,//截取整个页面
            //    OmitBackground = false,
            //    Quality = 100,//截取质量0-100（png不可用）
            //    Type = ScreenshotType.Jpeg
            //};

            //Console.WriteLine("===========截图base64开始==========");
            var fileName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshot", $"Screenshot-{DateTime.Now:yyyyMMddHHmmss}");
            //await page.ScreenshotAsync($"{fileName}.jpg", screenshotOptions);
            //var imgBase64 = await page.ScreenshotBase64Async(screenshotOptions);
            //await System.IO.File.WriteAllTextAsync($"{fileName}.txt", imgBase64);

            Console.WriteLine("===========截图pdf开始==========");
            var pdfOptions = new PdfOptions
            {
                DisplayHeaderFooter = false,//是否显示页眉页脚
                HeaderTemplate = "页头文本",
                FooterTemplate = "页脚文本",
                Format = PuppeteerSharp.Media.PaperFormat.A4,
                Landscape = false,//纸张方向，false-垂直，true-水平
                MarginOptions = new PuppeteerSharp.Media.MarginOptions { Bottom = "0px", Left = "0px", Right = "0px", Top = "0px" },//纸张边距
                Scale = 1m,//缩放 
            };

            await page.PdfAsync($"{fileName}.pdf", pdfOptions);
        }
    }
}
