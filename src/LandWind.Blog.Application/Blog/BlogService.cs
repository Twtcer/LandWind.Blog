using System;
using System.Linq;
using System.Threading.Tasks;
using LandWind.Blog.Core.Domain.Entities;
using LandWind.Blog.Core.Domain.Repositories;
using LandWind.Blog.Core.Dto.Blog;
using LandWind.Blog.Core.Extensions;
using LandWind.Blog.Core.Response.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace LandWind.Blog.Application.Blog
{
    /// <summary>
    /// BlogService
    /// </summary>
    public partial class BlogService : BlogAppServiceBase
    {
        protected readonly IPostRepository _postRepository;
        protected readonly ICategoryRepository _categoryRepository;
        protected readonly ITagRepository _tagRepository;
        protected readonly IFriendLinkRepository _friendLinksRepository;

        public BlogService()
        {
            _categoryRepository = ServiceProvider.GetRequiredService<ICategoryRepository>();
            _postRepository = ServiceProvider.GetRequiredService<IPostRepository>();
            _tagRepository = ServiceProvider.GetRequiredService<ITagRepository>();
            _friendLinksRepository = ServiceProvider.GetRequiredService<IFriendLinkRepository>();
        }
    }

    public class StatisticsService : BlogService, IStatisticsService
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [Route("api/blog/statistics")]
        public async Task<ResponseResult<Tuple<int, int, int>>> GetStatisticsAsync()
        {
            var response = new ResponseResult<Tuple<int, int, int>>();

            var postCount = await _postRepository.GetCountAsync();
            var categoryCount = await _categoryRepository.GetCountAsync();
            var tagCount = await _tagRepository.GetCountAsync();

            response.Result = new Tuple<int, int, int>(postCount.To<int>(), categoryCount.To<int>(), tagCount.To<int>());
            return response;
        }
    }

    public class BlogPostService : BlogService, IBlogPostService
    {
        #region Public
        public Task<ResponseResult<PagedList<QueryPostDto>>> QueryAsync(PagingInput input)
        {
            throw new NotImplementedException();
        }
        public Task<ResponseResult<PagedList<QueryPostDto>>> QueryAsync(int page, int limit, Func<Task<ResponseResult<PagedList<QueryPostDto>>>> func)
        {
            throw new NotImplementedException();
        }

        [Authorize]
        [Route("api/blog/post")]
        public Task<ResponseResult<PostDto>> GetByUrlAsync(string url)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Admin

        /// <summary>
        /// Add post
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/blog/post")]
        public async Task<ResponseResult> InsertAsync(CreatePostInput input)
        {
            var response = new ResponseResult();
            var tags = await _tagRepository.GetListAsync();
            var newTags = input.Tags.Where(t => !tags.Any(x => x.Name == t)).Select(a => new Tag { Name = a, Alias = a.ToLower() });
            if (newTags.Any())
            {
                await _tagRepository.InsertManyAsync(newTags);
            }

            var post = ObjectMapper.Map<CreatePostInput, Post>(input);
            post.Url = input.Url.GeneratePostUrl(input.CreationTime);

            await _postRepository.InsertAsync(post);

            return response;
        }

        /// <summary>
        /// Delete post by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/blog/post/{id}")]
        public async Task<ResponseResult> DeleteAsync(int id)
        {
            var response = new ResponseResult();

            var post = await _postRepository.FindAsync(id);
            if (post is null)
            {
                response.IsFailed($"The post (id:{id}) not exists!");
                return response;
            }

            await _postRepository.DeleteAsync(id);

            return response;
        }

        /// <summary>
        /// Get post by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/blog/post/{id}")]
        public async Task<ResponseResult<PostDto>> GetAsync(int id)
        {
            var response = new ResponseResult<PostDto>();
            var post = await _postRepository.FindAsync(id);

            if (post is null)
            {
                response.IsFailed($"The post id :{id} not exist!");
                return response;
            }

            var result = ObjectMapper.Map<Post, PostDto>(post);
            result.Url = result.Url.Split("-").Last();
            response.Result = result;

            return response;
        }

        [Authorize]
        [Route("api/blog/post/{id}")]
        public async Task<ResponseResult<string>> UpdateAsync(int id, UpdatePostInput input)
        {
            var res = ResponseResult<string>.Instance;
            var old = await _postRepository.FindAsync(id);
            if (old is null)
            {
                res.IsFailed($"The post id:{id} not exists");
                return res;
            }

            var tags = await _tagRepository.GetListAsync();
            var newTags = input.Tags.Where(x => !tags.Any(a => a.Name == x)).Select(a => new Tag { Name = a, Alias = a.ToLower() });

            if (newTags.Any())
            {
                await _tagRepository.InsertManyAsync(newTags);
            }

            var post = ObjectMapper.Map<UpdatePostInput, Post>(input);
            post.Url = input.Url.GeneratePostUrl(input.CreationTime);
            await _postRepository.UpdateAsync(post);

            return res;
        }
        #endregion
    }
}
