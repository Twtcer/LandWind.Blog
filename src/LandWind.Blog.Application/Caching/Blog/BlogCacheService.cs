using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LandWind.Blog.Core.Caching;
using LandWind.Blog.Core.Dto.Blog;
using LandWind.Blog.Core.Response.Base;

namespace LandWind.Blog.Application.Caching
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BlogCacheService<DtoT> : CachingServiceBase, IBlogCacheService<DtoT>
    {
        private static readonly Dictionary<Type, string> CacheKeyDict = new Dictionary<Type, string> {
            { typeof(QueryPostDto),ApplicationCachingConsts.CacheKeys.GetPostsList()},
            { typeof(QueryTagDto),ApplicationCachingConsts.CacheKeys.GetTags()},
            { typeof(QueryCategoryDto),ApplicationCachingConsts.CacheKeys.GetCategories()},
            { typeof(QueryFriendLinkDto),ApplicationCachingConsts.CacheKeys.GetFriendLinks()}
        }; 
        protected static int cacheStrategy = ApplicationCachingConsts.CacheStrategy.HalfDay;

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public async Task<ResponseResult<List<DtoT>>> GetListAsync(Func<Task<ResponseResult<List<DtoT>>>> func)
        {
            var key = CacheKeyDict[typeof(DtoT)];  
            return await Cache.GetOrAddAsync(key, func, cacheStrategy);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public async Task<ResponseResult<PagedList<DtoT>>> GetPageAsync(int page, int limit, Func<Task<ResponseResult<PagedList<DtoT>>>> func)
        {
            var key = CacheKeyDict[typeof(DtoT)];
            if (typeof(DtoT) == typeof(QueryPostDto))
            {
                key = ApplicationCachingConsts.CacheKeys.GetPosts(page, limit);
            }
            return await Cache.GetOrAddAsync(key, func, cacheStrategy);
        }
    }

    /// <summary>
    /// Blog post cache
    /// </summary>
    public class BlogPostCacheService : BlogCacheService<QueryPostDto>, IBlogPostCacheService
    {
        /// <summary>
        /// Get posts by url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public async Task<ResponseResult<PostDetailDto>> GetPostByUrlAsync(string url, Func<Task<ResponseResult<PostDetailDto>>> func)
        {
            return await Cache.GetOrAddAsync(ApplicationCachingConsts.CacheKeys.GetPostByUrl(url), func, cacheStrategy);
        }

        /// <summary>
        /// Get posts by category
        /// </summary>
        /// <param name="category"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public async Task<ResponseResult<List<QueryPostDto>>> GetPostsByCategoryAsync(string category, Func<Task<ResponseResult<List<QueryPostDto>>>> func)
        {
            return await Cache.GetOrAddAsync(ApplicationCachingConsts.CacheKeys.GetPostsByCategory(category), func, cacheStrategy);
        }

        /// <summary>
        /// Get posts by tag
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public async Task<ResponseResult<List<QueryPostDto>>> GetPostsByTagAsync(string tag, Func<Task<ResponseResult<List<QueryPostDto>>>> func)
        {
            return await Cache.GetOrAddAsync(ApplicationCachingConsts.CacheKeys.GetPostsByTag(tag), func, cacheStrategy);
        }
    }

    /// <summary>
    /// Blog tag cache
    /// </summary>
    public class BlogTagCacheService : BlogCacheService<QueryTagDto>, IBlogTagCacheService
    {

    }

    /// <summary>
    /// Blog category cache
    /// </summary>
    public class BlogCategoryService : BlogCacheService<QueryCategoryDto>, IBlogCategoryCacheService
    {
    }

    /// <summary>
    /// Blog FriendLink cache
    /// </summary>
    public class BlogFriendLinkService : BlogCacheService<QueryFriendLinkDto>, IBlogFriendLinkCacheService
    {

    }
}
