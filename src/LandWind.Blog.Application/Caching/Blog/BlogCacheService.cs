using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LandWind.Blog.Core.Caching;
using LandWind.Blog.Core.Dto.Blog;
using LandWind.Blog.Core.Response.Base;

namespace LandWind.Blog.Application.Caching
{
    public partial class BlogCacheService : CachingServiceBase
    {
        private static readonly string QueryDtosKey = "Blog:{0}:Query{0}s-{1}-{2}";
    }

    //TODO:完善Blog Cache 服务
    public class BlogPostCacheService : BlogCacheService, IBlogPostCacheService
    {
        public Task<ResponseResult<List<QueryPostDto>>> GetListAsync(Func<Task<ResponseResult<List<QueryPostDto>>>> func)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseResult<PagedList<QueryPostDto>>> GetPageAsync(int page, int limit, Func<Task<ResponseResult<PagedList<QueryPostDto>>>> func)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseResult<PostDetailDto>> GetPostByUrlAsync(string url, Func<Task<ResponseResult<PostDetailDto>>> func)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseResult<List<QueryPostDto>>> GetPostsByCategoryAsync(string category, Func<Task<ResponseResult<List<QueryPostDto>>>> func)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseResult<List<QueryPostDto>>> GetPostsByTagAsync(string tag, Func<Task<ResponseResult<List<QueryPostDto>>>> func)
        {
            throw new NotImplementedException();
        }
    }

    public class BlogTagCacheService : CachingServiceBase, IBlogTagCacheService
    {
        public async Task<ResponseResult<List<GetTagDto>>> GetListAsync(Func<Task<ResponseResult<List<GetTagDto>>>> func)
        {
            return await Cache.GetOrAddAsync(ApplicationCachingConsts.CacheKeys.GetTags(), func, ApplicationCachingConsts.CacheStrategy.HalfDay);
        }

        public Task<ResponseResult<PagedList<GetTagDto>>> GetPageAsync(int page, int limit, Func<Task<ResponseResult<PagedList<GetTagDto>>>> func)
        {
            throw new NotImplementedException();
        }
    }

    public class BlogCategoryService : CachingServiceBase, IBlogCategoryCacheService
    {
        public Task<ResponseResult<List<GetCategoryDto>>> GetListAsync(Func<Task<ResponseResult<List<GetCategoryDto>>>> func)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseResult<PagedList<GetCategoryDto>>> GetPageAsync(int page, int limit, Func<Task<ResponseResult<PagedList<GetCategoryDto>>>> func)
        {
            throw new NotImplementedException();
        }
    }

    public class BlogFriendLinkService : CachingServiceBase, IBlogFriendLinkCacheService
    {
        public Task<ResponseResult<List<FriendLinkDto>>> GetListAsync(Func<Task<ResponseResult<List<FriendLinkDto>>>> func)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseResult<PagedList<FriendLinkDto>>> GetPageAsync(int page, int limit, Func<Task<ResponseResult<PagedList<FriendLinkDto>>>> func)
        {
            throw new NotImplementedException();
        }
    }
}
