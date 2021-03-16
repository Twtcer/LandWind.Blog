using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IP2Region;
using LandWind.Blog.Core.DataAnnotation.Output;
using LandWind.Blog.Core.Dto.Tools;
using LandWind.Blog.Core.Extensions;
using LandWind.Blog.Core.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace LandWind.Blog.Application.Tool
{
    /// <summary>
    ///  ToolService
    /// </summary>
    public class ToolService : BlogAppServiceBase, IToolService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TencentCloudOptions _tencentCloudOptions;
        private readonly FtqqOptions _ftqqOptions;

        public ToolService(IHttpClientFactory httpClient,
                IHttpContextAccessor httpContextAccessor,
                IOptions<TencentCloudOptions> tencentCloudOptions,
                 IOptions<FtqqOptions> ftqqOptions
            )
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _tencentCloudOptions = tencentCloudOptions.Value;
            _ftqqOptions = ftqqOptions.Value;
        }

        /// <summary>
        /// 获取必应背景图
        /// </summary>
        /// <returns></returns>
        [Route("api/tool/bing/img")]
        public async Task<FileContentResult> GetBingBackgroundImgAsync()
        {
            var ret = (await GetBingBackgroundUrlAsync()) as ResponseOutput<string>;

            using var client = _httpClient.CreateClient();
            var bytes = await client.GetByteArrayAsync(ret.Data);

            return new FileContentResult(bytes, "image/jpeg");
        }

        /// <summary>
        /// 获取必应背景图地址
        /// </summary>
        /// <returns></returns>
        [Route("api/tool/bing/url")]
        public async Task<IResponseOutput> GetBingBackgroundUrlAsync()
        { 
            var apiUrl = "https://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&pid=hp&FORM=BEHPTB";

            using var client = _httpClient.CreateClient();
            var json = await client.GetStringAsync(apiUrl); 
            var obj = JObject.Parse(json); 

            return ResponseOutput.Ok($"https://cn.bing.com{obj["images"].First()["url"]}");
        }

        /// <summary>
        /// get img 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [Route("api/tool/img")]
        public async Task<FileContentResult> GetImgAsync(string url)
        {
            using var client = _httpClient.CreateClient();
            var bytes = await client.GetByteArrayAsync(url);

            return new FileContentResult(bytes, "image/jpeg");
        }

        /// <summary>
        /// get ip region 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/tool/ip2region")]
        public async Task<IResponseOutput> Ip2RegionAsync(string ip)
        { 
            if (ip.IsNullOrEmpty())
            {
                ip = _httpContextAccessor.HttpContext.Request.Headers["X-Real-IP"].FirstOrDefault() ??
                         _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault() ??
                     _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
            else
            {
                if (!ip.IsIp())
                { 
                    return ResponseOutput.NotOk("The ip address error.");
                }
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources/ip2region.db");
            using var _search = new DbSearcher(path);
            var block = await _search.BinarySearchAsync(ip);
            var region = block.Region.Split("|").Distinct().Where(x => x != "0").ToList();
            region.AddFirst(ip);

            return ResponseOutput.Ok(region);
        }

        /// <summary>
        /// send msg by wechat 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("api/tool/sendmsg")]
        public async Task<IResponseOutput> SendMessageAsync(SendMessageInput input)
        { 
            var content = new StringContent($"text={input.Text}&desp={input.Desc}"); 
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");

            using var client = _httpClient.CreateClient();
            await client.PostAsync($"{_ftqqOptions.ApiUrl}/{_ftqqOptions.Token}.send", content);

            return ResponseOutput.Ok(content);
        }
    }
}
