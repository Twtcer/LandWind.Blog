using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using LandWind.Blog.Application.Contracts.Jobs;
using LandWind.Blog.Core.Extensions;
using LandWind.Blog.Domain.Entities;
using LandWind.Blog.Domain.Repositories.Interface;
using LandWind.Blog.Domain.Shared.Enum;

namespace LandWind.Blog.BackgroundJobs.Jobs
{
    public class HotNewsJob : IBackgroundJob
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IHotNewsRepository _hotNewsRepository;

        public HotNewsJob(IHttpClientFactory httpClient, IHotNewsRepository hotNewsRepository)
        {
            _httpClient = httpClient;
            _hotNewsRepository = hotNewsRepository;
        }

        public async Task ExecuteAsync()
        {
            var hotnewsUrls = new List<HotNewsJobItem<string>>
                {
                    new HotNewsJobItem<string> { Result = "https://www.cnblogs.com", Source = HotNewsEnum.cnblogs },
                    new HotNewsJobItem<string> { Result = "https://www.v2ex.com/?tab=hot", Source = HotNewsEnum.v2ex },
                    new HotNewsJobItem<string> { Result = "https://segmentfault.com/hottest", Source = HotNewsEnum.segmentfault },
                    new HotNewsJobItem<string> { Result = "https://web-api.juejin.im/query", Source = HotNewsEnum.juejin },
                    new HotNewsJobItem<string> { Result = "https://weixin.sogou.com", Source = HotNewsEnum.weixinhot },
                    new HotNewsJobItem<string> { Result = "https://www.douban.com/group/explore", Source = HotNewsEnum.douban },
                    new HotNewsJobItem<string> { Result = "https://www.ithome.com", Source = HotNewsEnum.ithome },
                    new HotNewsJobItem<string> { Result = "https://36kr.com/newsflashes", Source = HotNewsEnum.kr36 },
                    new HotNewsJobItem<string> { Result = "http://tieba.baidu.com/hottopic/browse/topicList", Source = HotNewsEnum.tieba },
                    new HotNewsJobItem<string> { Result = "http://top.baidu.com/buzz?b=341", Source = HotNewsEnum.baidu },
                    new HotNewsJobItem<string> { Result = "https://s.weibo.com/top/summary/summary", Source = HotNewsEnum.weibo },
                    new HotNewsJobItem<string> { Result = "https://www.zhihu.com/api/v3/feed/topstory/hot-lists/total?limit=50&desktop=true", Source = HotNewsEnum.zhihu },
                    new HotNewsJobItem<string> { Result = "https://daily.zhihu.com", Source = HotNewsEnum.zhihudaily },
                    new HotNewsJobItem<string> { Result = "http://news.163.com/special/0001386F/rank_whole.html", Source = HotNewsEnum.news163 },
                    new HotNewsJobItem<string> { Result = "https://github.com/trending", Source = HotNewsEnum.github },
                    new HotNewsJobItem<string> { Result = "https://www.iesdouyin.com/web/api/v2/hotsearch/billboard/word", Source = HotNewsEnum.ticktock_hot }
                };

            var web = new HtmlWeb();
            var tasks = new List<Task<HotNewsJobItem<object>>>();

            hotnewsUrls.ForEach(item =>
            {
                var task = Task.Run(async () =>
                {
                    var obj = new object();
                    if (item.Source == HotNewsEnum.juejin)
                    {
                        using var client = _httpClient.CreateClient();
                        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.14 Safari/537.36 Edg/83.0.478.13");
                        client.DefaultRequestHeaders.Add("X-Agent", "Juejin/Web");
                        var data = "{\"extensions\":{\"query\":{ \"id\":\"21207e9ddb1de777adeaca7a2fb38030\"}},\"operationName\":\"\",\"query\":\"\",\"variables\":{ \"first\":20,\"after\":\"\",\"order\":\"THREE_DAYS_HOTTEST\"}}";
                        var buffer = data.ToUtf8();
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        var httpResponse = await client.PostAsync(item.Result, byteContent);
                        obj = await httpResponse.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                        obj = await web.LoadFromWebAsync(item.Result, (item.Source == HotNewsEnum.baidu || item.Source == HotNewsEnum.news163) ? Encoding.GetEncoding("GB2312") : Encoding.UTF8);
                    }

                    return new HotNewsJobItem<object>
                    {
                        Result = obj,
                        Source = item.Source
                    };
                });
                tasks.Add(task);
            });
            Task.WaitAll(tasks.ToArray());

            var hotNews = new List<HotNews>();
            foreach (var task in tasks)
            {
                var item = await task;
                var sourceId = (int)item.Source;

                if (hotNews.Any())
                {
                    await _hotNewsRepository.DeleteAsync(a => true);
                    await _hotNewsRepository.BulkInsertAsync(hotNews);
                }
            }
        }
    }
}
