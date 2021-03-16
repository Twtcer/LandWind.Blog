using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LandWind.Blog.Core.Dto.Tools;
using LandWind.Blog.Core.DataAnnotation.Output;

namespace LandWind.Blog.Application.Tool
{
    public interface IToolService
    {
        Task<IResponseOutput> GetBingBackgroundUrlAsync();

        Task<FileContentResult> GetBingBackgroundImgAsync();

        Task<IResponseOutput> Ip2RegionAsync(string ip);

        Task<IResponseOutput> SendMessageAsync(SendMessageInput input);

        Task<FileContentResult> GetImgAsync(string url);

        //Task<ResponseResult<PurgeUrlsCacheResponse>> PurgeCdnUrlsAsync(List<string> urls);

        //Task<ResponseResult<PurgePathCacheResponse>> PurgeCdnPathsAsync(List<string> paths);

        //Task<ResponseResult<PushUrlsCacheResponse>> PushCdnUrlsAsync(List<string> urls);
    }
}
