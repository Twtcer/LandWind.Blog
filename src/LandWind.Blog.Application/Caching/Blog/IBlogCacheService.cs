using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LandWind.Blog.Core.Dto.Blog;
using LandWind.Blog.Core.Response.Base;

namespace LandWind.Blog.Core.Caching
{
    public interface IBlogCacheService<QueryDto> : ICacheRemoveService
    {
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<ResponseResult<PagedList<QueryDto>>> GetPageAsync(int page, int limit, Func<Task<ResponseResult<PagedList<QueryDto>>>> func);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<ResponseResult<List<QueryDto>>> GetListAsync(Func<Task<ResponseResult<List<QueryDto>>>> func);
    }

    /// <summary>
    /// IBlogPostCacheService
    /// </summary>
    public interface IBlogPostCacheService : IBlogCacheService<QueryPostDto>
    {
        /// <summary>
        /// Get post by url from the cache.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<ResponseResult<PostDetailDto>> GetPostByUrlAsync(string url, Func<Task<ResponseResult<PostDetailDto>>> func);

        /// <summary>
        /// Get the list of posts by category from the cache.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<ResponseResult<List<QueryPostDto>>> GetPostsByCategoryAsync(string category, Func<Task<ResponseResult<List<QueryPostDto>>>> func);

        /// <summary>
        /// Get the list of posts by tag from the cache.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<ResponseResult<List<QueryPostDto>>> GetPostsByTagAsync(string tag, Func<Task<ResponseResult<List<QueryPostDto>>>> func);
    }

    /// <summary>
    /// IBlogTagCacheService
    /// </summary>
    public interface IBlogTagCacheService:IBlogCacheService<GetTagDto>
    {

    }

    /// <summary>
    /// IBlogCategoryCacheService
    /// </summary>  
    public interface IBlogCategoryCacheService : IBlogCacheService<GetCategoryDto>
    {

    }

    /// <summary>
    /// IBlogFriendLinkCacheService
    /// </summary>
    public interface IBlogFriendLinkCacheService : IBlogCacheService<FriendLinkDto>
    {

    }
}
