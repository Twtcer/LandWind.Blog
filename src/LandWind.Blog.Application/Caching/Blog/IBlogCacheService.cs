using System;
using System.Threading.Tasks;
using LandWind.Blog.Core.Dto.Blog;
using LandWind.Blog.Core.Response.Base;

namespace LandWind.Blog.Core.Caching
{
    public partial interface IBlogCacheService<QueryDto>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        Task<ResponseResult<PagedList<QueryDto>>> QueryAsync(PagingInput input, Func<Task<ResponseResult<PagedList<QueryDto>>>> factory);
    }

    public partial interface IBlogPostCacheService : IBlogCacheService<QueryPostDto>
    {
        
    }
}
