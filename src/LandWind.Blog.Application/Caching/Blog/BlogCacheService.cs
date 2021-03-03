using System;
using System.Threading.Tasks;
using LandWind.Blog.Core.Caching;
using LandWind.Blog.Core.Dto.Blog;
using LandWind.Blog.Core.Response.Base;

namespace LandWind.Blog.Application.Caching
{
    /// <summary>
    /// 博客缓存服务
    /// </summary>
    /// <typeparam name="QueryDto"></typeparam>
    public partial class BlogCacheService<QueryDto> : CachingServiceBase, IBlogCacheService<QueryDto>
    {
        private static readonly string QueryDtosKey = "Blog:{0}:Query{0}s-{1}-{2}";

        public async Task<ResponseResult<PagedList<QueryDto>>> QueryAsync(PagingInput input, Func<Task<ResponseResult<PagedList<QueryDto>>>> factory)
        {
            throw new NotImplementedException();           
           //await Cache.GetOrAddAsync(QueryDtosKey.FormatWith(""),factory, )
        }
    }

    public class BlogPostCacheService  : BlogCacheService<QueryPostDto>
    {
        public  async Task<ResponseResult<PagedList<QueryPostDto>>> QueryAsync(PagingInput input, Func<Task<ResponseResult<PagedList<QueryPostDto>>>> factory)
        {
            return await base.QueryAsync(input,factory);
        }
    }

}
