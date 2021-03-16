using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LandWind.Blog.Core.DataAnnotation.Output;
using LandWind.Blog.Core.Dto.Blog;

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
        Task<IResponseOutput> GetPageAsync(int page, int limit, Func<Task<IResponseOutput<QueryDto>>> func);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<IResponseOutput> GetListAsync(Func<Task<IResponseOutput<List<QueryDto>>>> func);
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
        Task<IResponseOutput> GetPostByUrlAsync(string url, Func<Task<IResponseOutput<PostDetailDto>>> func);

        /// <summary>
        /// Get the list of posts by category from the cache.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<IResponseOutput> GetPostsByCategoryAsync(string category, Func<Task<IResponseOutput<List<QueryPostDto>>>> func);

        /// <summary>
        /// Get the list of posts by tag from the cache.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<IResponseOutput> GetPostsByTagAsync(string tag, Func<Task<IResponseOutput<List<QueryPostDto>>>> func);
    }

    /// <summary>
    /// IBlogTagCacheService
    /// </summary>
    public interface IBlogTagCacheService:IBlogCacheService<QueryTagDto>
    {

    }

    /// <summary>
    /// IBlogCategoryCacheService
    /// </summary>  
    public interface IBlogCategoryCacheService : IBlogCacheService<QueryCategoryDto>
    {

    }

    /// <summary>
    /// IBlogFriendLinkCacheService
    /// </summary>
    public interface IBlogFriendLinkCacheService : IBlogCacheService<QueryFriendLinkDto>
    {

    }
}
