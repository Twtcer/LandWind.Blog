using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using LandWind.Blog.Core.Response.Base;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LandWind.Blog.Core.Dto.Tools;

namespace LandWind.Blog.Application.Tool
{
    public interface IToolService
    {
        Task<ResponseResult<string>> GetBingBackgroundUrlAsync();

        Task<FileContentResult> GetBingBackgroundImgAsync();

        Task<ResponseResult<List<string>>> Ip2RegionAsync(string ip);

        Task<ResponseResult> SendMessageAsync(SendMessageInput input);

        Task<FileContentResult> GetImgAsync(string url);

        //Task<ResponseResult<PurgeUrlsCacheResponse>> PurgeCdnUrlsAsync(List<string> urls);

        //Task<ResponseResult<PurgePathCacheResponse>> PurgeCdnPathsAsync(List<string> paths);

        //Task<ResponseResult<PushUrlsCacheResponse>> PushCdnUrlsAsync(List<string> urls);
    }
}
