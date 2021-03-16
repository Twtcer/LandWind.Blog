using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LandWind.Blog.Core.Caching;
using LandWind.Blog.Core.DataAnnotation.Output;
using LandWind.Blog.Core.Dto.Blog;

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
        public async Task<IResponseOutput> GetListAsync(Func<Task<IResponseOutput<List<DtoT>>>> func)
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
        public async Task<IResponseOutput> GetPageAsync(int page, int limit, Func<Task<IResponseOutput<DtoT>>> func)
        {
            var key = CacheKeyDict[typeof(DtoT)];
            if (typeof(DtoT) == typeof(QueryPostDto))
            {
                key = ApplicationCachingConsts.CacheKeys.GetPosts(page, limit);
            }
            return await Cache.GetOrAddAsync(key, func, cacheStrategy);
        }

        Task<IResponseOutput> IBlogCacheService<DtoT>.GetListAsync(Func<Task<IResponseOutput<List<DtoT>>>> func)
        {
            throw new NotImplementedException();
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
        public async Task<IResponseOutput> GetPostByUrlAsync(string url, Func<Task<IResponseOutput<PostDetailDto>>> func)
        {
            return await Cache.GetOrAddAsync(ApplicationCachingConsts.CacheKeys.GetPostByUrl(url), func, cacheStrategy);
        }

        /// <summary>
        /// Get posts by category
        /// </summary>
        /// <param name="category"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public async Task<IResponseOutput> GetPostsByCategoryAsync(string category, Func<Task<IResponseOutput<List<QueryPostDto>>>> func)
        {
            return await Cache.GetOrAddAsync(ApplicationCachingConsts.CacheKeys.GetPostsByCategory(category), func, cacheStrategy);
        }

        /// <summary>
        /// Get posts by tag
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public async Task<IResponseOutput> GetPostsByTagAsync(string tag, Func<Task<IResponseOutput<List<QueryPostDto>>>> func)
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
