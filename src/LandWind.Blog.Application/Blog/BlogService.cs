//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using LandWind.Blog.Core.Caching;
//using LandWind.Blog.Core.DataAnnotation.Output;
//using LandWind.Blog.Core.Domain.Entities;
//using LandWind.Blog.Core.Domain.Repositories;
//using LandWind.Blog.Core.Dto.Blog;
//using LandWind.Blog.Core.Extensions;
//using LandWind.Blog.Core.Response.Base;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.DependencyInjection;

//namespace LandWind.Blog.Application.Blog
//{
//    #region Base
//    /// <summary>
//    /// BlogService
//    /// </summary>
//    public class BlogServiceBase : BlogAppServiceBase
//    {
//        protected readonly IPostRepository _postRepository;
//        protected readonly ICategoryRepository _categoryRepository;
//        protected readonly ITagRepository _tagRepository;
//        protected readonly IFriendLinkRepository _friendLinksRepository;

//        public BlogServiceBase()
//        {
//            _categoryRepository = ServiceProvider.GetRequiredService<ICategoryRepository>();
//            _postRepository = ServiceProvider.GetRequiredService<IPostRepository>();
//            _tagRepository = ServiceProvider.GetRequiredService<ITagRepository>();
//            _friendLinksRepository = ServiceProvider.GetRequiredService<IFriendLinkRepository>();
//        }

//    }

//    public class StatisticsService : BlogServiceBase, IStatisticsService
//    {
//        /// <summary>
//        ///
//        /// </summary>
//        /// <returns></returns>
//        [Route("api/blog/statistics")]
//        public async Task<IResponseOutput<Tuple<int, int, int>>> GetStatisticsAsync()
//        {
//            var response = new IResponseOutput<Tuple<int, int, int>>();

//            var postCount = await _postRepository.GetCountAsync();
//            var categoryCount = await _categoryRepository.GetCountAsync();
//            var tagCount = await _tagRepository.GetCountAsync();

//            response.Result = new Tuple<int, int, int>(postCount.To<int>(), categoryCount.To<int>(), tagCount.To<int>());
//            return response;
//        }
//    }
//    #endregion

//    #region Impl
//    public class BlogPostService : BlogServiceBase, IBlogPostService
//    {
//        private readonly IBlogPostCacheService cacheService;

//        #region Public 
//        /// <summary>
//        /// Get post page by paging
//        /// </summary>
//        /// <param name="page"></param>
//        /// <param name="limit"></param>
//        /// <returns></returns>
//        [Authorize]
//        [Route("api/meowv/blog/admin/posts/{page}/{limit}")]
//        public async Task<IResponseOutput<PagedList<QueryPostDto>>> QueryAsync(int page = 1, int limit = 10)
//        {
//            var response = new IResponseOutput<PagedList<QueryPostDto>>();

//            var result = await _postRepository.GetPagedListAsync(page, limit);
//            var total = result.Item1;
//            var posts = ObjectMapper.Map<List<Post>, List<QueryPostDto>>(result.Item2);
//            response.Result = new PagedList<QueryPostDto>(total, posts);

//            return response;
//        }
         
//        public Task<IResponseOutput<List<QueryPostDto>>> QueryAsync()
//        {
//            throw new NotImplementedException();
//        }
         
//        [Route("api/blog/post")]
//        public Task<IResponseOutput<PostDetailDto>> GetByUrlAsync(string url)
//        {
//            return cacheService.GetPostByUrlAsync(url, async () =>
//            {
//                var ret = IResponseOutput<PostDetailDto>.Instance;
//                var post = await _postRepository.FindAsync(a => a.Url == url);

//                if (post is null)
//                {
//                    ret.IsFailed($"The post url not exists.");
//                    return ret;
//                }

//                var previous = _postRepository.Where(a => a.CreationTime > post.CreationTime).Take(1).Select(a => new PostPagedDto
//                {
//                    Title = a.Title,
//                    Url = a.Url,
//                }).FirstOrDefault();
//                var next = _postRepository.Where(a => a.CreationTime < post.CreationTime).Take(1).Select(a => new PostPagedDto
//                {
//                    Title = a.Title,
//                    Url = a.Url
//                }).FirstOrDefault();

//                var result = ObjectMapper.Map<Post, PostDetailDto>(post);
//                result.Previous = previous;
//                result.Next = next;
//                ret.Result = result;

//                return ret;
//            });
//        } 

//        /// <summary>
//        ///  Get posts by category
//        /// </summary>
//        /// <param name="category"></param>
//        /// <returns></returns>
//        public async Task<IResponseOutput<List<QueryPostDto>>> GetPostsByCategoryAsync(string category)
//        {
//            return await cacheService.GetPostsByCategoryAsync(category, async () =>
//            {
//                var response = new IResponseOutput<List<QueryPostDto>>();

//                var cate = await _categoryRepository.FindAsync(a => a.Alias == category);

//                if (cate is null)
//                {
//                    response.IsFailed($"The category :{category} not exists.");
//                    return response;
//                }

//                var posts = await _postRepository.GetListByCategoryAsync(category); 
//                response.IsSuccess(GetPostList(posts), cate.Name);

//                return response;
//            });
//        } 

//        public Task<IResponseOutput<List<QueryPostDto>>> GetPostsByTagAsync(string tag)
//        {
//            throw new NotImplementedException();
//        }

//        private List<QueryPostDto> GetPostList(List<Post> posts)
//        {
//            return ObjectMapper.Map<List<Post>, List<PostBriefDto>>(posts)
//                .GroupBy(a => a.Year)
//                .Select(a => new QueryPostDto
//                {
//                    Year= a.Key,
//                    Posts = a
//                }).ToList();
//        }

//        #endregion

//        #region Authorize

//        /// <summary>
//        /// Add post
//        /// </summary>
//        /// <param name="input"></param>
//        /// <returns></returns>
//        [Authorize]
//        [Route("api/blog/post")]
//        public async Task<IResponseOutput> InsertAsync(CreatePostInput input)
//        {
//            var response = new IResponseOutput();
//            var tags = await _tagRepository.GetListAsync();
//            var newTags = input.Tags.Where(t => !tags.Any(x => x.Name == t)).Select(a => new Tag { Name = a, Alias = a.ToLower() });
//            if (newTags.Any())
//            {
//                await _tagRepository.InsertManyAsync(newTags);
//            }

//            var post = ObjectMapper.Map<CreatePostInput, Post>(input);
//            post.Url = input.Url.GeneratePostUrl(input.CreationTime);

//            await _postRepository.InsertAsync(post);

//            return response;
//        }

//        /// <summary>
//        /// Delete post by id
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        [Authorize]
//        [Route("api/blog/post/{id}")]
//        public async Task<IResponseOutput> DeleteAsync(int id)
//        {
//            var response = new IResponseOutput();

//            var post = await _postRepository.FindAsync(id);
//            if (post is null)
//            {
//                response.IsFailed($"The post (id:{id}) not exists!");
//                return response;
//            }

//            await _postRepository.DeleteAsync(id);

//            return response;
//        }

//        /// <summary>
//        /// Get post by id
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        [Authorize]
//        [Route("api/blog/post/{id}")]
//        public async Task<IResponseOutput<PostDto>> GetAsync(int id)
//        {
//            var response = new IResponseOutput<PostDto>();
//            var post = await _postRepository.FindAsync(id);

//            if (post is null)
//            {
//                response.IsFailed($"The post id :{id} not exist!");
//                return response;
//            }

//            var result = ObjectMapper.Map<Post, PostDto>(post);
//            result.Url = result.Url.Split("-").Last();
//            response.Result = result;

//            return response;
//        }

//        [Authorize]
//        [Route("api/blog/post/{id}")]
//        public async Task<IResponseOutput> UpdateAsync(int id, UpdatePostInput input)
//        {
//            var res = IResponseOutput<string>.Instance;
//            var old = await _postRepository.FindAsync(id);
//            if (old is null)
//            {
//                res.IsFailed($"The post id:{id} not exists");
//                return res;
//            }

//            var tags = await _tagRepository.GetListAsync();
//            var newTags = input.Tags.Where(x => !tags.Any(a => a.Name == x)).Select(a => new Tag { Name = a, Alias = a.ToLower() });

//            if (newTags.Any())
//            {
//                await _tagRepository.InsertManyAsync(newTags);
//            }

//            var post = ObjectMapper.Map<UpdatePostInput, Post>(input);
//            post.Url = input.Url.GeneratePostUrl(input.CreationTime);
//            await _postRepository.UpdateAsync(post);

//            return res;
//        } 

//        #endregion
//    }

//    public class BlogTagService : BlogServiceBase, IBlogTagsService
//    {
//        private readonly IBlogCacheService<QueryTagDto> cacheService;

//        #region Authorize
//        /// <summary>
//        /// Delete the tag
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        [Authorize]
//        [Route("api/blog/tag/{id}")]
//        public async Task<IResponseOutput> DeleteAsync(int id)
//        {
//            var ret = IResponseOutput.Instance;
//            var tag = await _tagRepository.FindAsync(id);

//            if (tag is null)
//            {
//                ret.IsFailed("The Tag is not exists.");
//                return ret;
//            }
//            await _tagRepository.DeleteAsync(id);

//            return ret;
//        }

//        /// <summary>
//        /// Find the tag by id
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        [Authorize]
//        [Route("api/blog/tag/{id}")]
//        public async Task<IResponseOutput<TagDto>> GetAsync(int id)
//        {
//            var tag = await _tagRepository.FindAsync(id);
//            if (tag is null)
//            {
//                throw new Exception("The Tag is not exists.");
//            }

//            var dto = ObjectMapper.Map<Tag, TagDto>(tag);

//            return new IResponseOutput<TagDto>(dto);
//        }

//        /// <summary>
//        /// Add tag
//        /// </summary>
//        /// <param name="input"></param>
//        /// <returns></returns>
//        [Authorize]
//        [Route("api/blog/tag")]
//        public async Task<IResponseOutput> InsertAsync(CreateTagInput input)
//        {
//            var ret = IResponseOutput.Instance;
//            var tag = await _tagRepository.FindAsync(x => x.Name == input.Name);

//            if (tag is not null)
//            {
//                ret.IsFailed($"The tag: {input.Name} already exists!");
//                return ret;
//            }

//            await _tagRepository.InsertAsync(new Tag { Name = input.Name, Alias = input.Alias });

//            return ret;
//        }

//        /// <summary>
//        /// Update tag
//        /// </summary>
//        /// <param name="id"></param>
//        /// <param name="inputT"></param>
//        /// <returns></returns>
//        [Authorize]
//        [Route("api/blog/tag/{id}")]
//        public async Task<IResponseOutput> UpdateAsync(int id, UpdateTagInput input)
//        {
//            var find = await _tagRepository.FindAsync(x => x.Name == input.Name);

//            if (find is not null)
//            {
//                throw new Exception($"The tag: {input.Name} already exists!");
//            }
//            var tag = ObjectMapper.Map(input, find);
//            await _tagRepository.UpdateAsync(tag);

//            return IResponseOutput.Instance;
//        }
//        #endregion

//        #region Public
//        public Task<IResponseOutput<PagedList<QueryTagDto>>> QueryAsync(int page = 1, int limit = 10)
//        {
//            return cacheService.GetPageAsync(page, limit, async () =>
//          {
//              var tags = await _tagRepository.GetListAsync();
//              var dtos = ObjectMapper.Map<List<Tag>, List<QueryTagDto>>(tags);
//              var ret = new IResponseOutput<PagedList<QueryTagDto>>();
//              ret.Result = new PagedList<QueryTagDto>(tags.Count, dtos);

//              return ret;
//          });
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        public async Task<IResponseOutput<List<QueryTagDto>>> QueryAsync()
//        {
//            return await cacheService.GetListAsync(async () =>
//            {
//                var tags = await _tagRepository.GetListAsync();
//                var data = tags.Select(a => new QueryTagDto
//                {
//                    Name = a.Name,
//                    Alias = a.Alias,
//                    Total = _postRepository.GetCountByTagAsync(a.Id).Result
//                }).ToList(); 

//                var ret = new IResponseOutput<List<QueryTagDto>>();
//                ret.Result = data;

//                return ret;
//            });
//        }
//        #endregion
//    }

//    public class BlogCategoryService : BlogServiceBase, IBlogCategoryService
//    {
//        #region Authorize
//        public Task<IResponseOutput> DeleteAsync(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IResponseOutput<CategoryDto>> GetAsync(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IResponseOutput> InsertAsync(CreateCategoryInput input)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IResponseOutput> UpdateAsync(int id, UpdateCategoryInput input)
//        {
//            throw new NotImplementedException();
//        }
//        #endregion
//        #region Public

//        public Task<IResponseOutput<PagedList<QueryCategoryDto>>> QueryAsync(int page = 1, int limit = 10)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IResponseOutput<List<QueryCategoryDto>>> QueryAsync()
//        {
//            throw new NotImplementedException();
//        }
//        #endregion
//    }

//    public class BlogFriendLinkService : BlogServiceBase, IBlogFriendLinkService
//    {
//        public Task<IResponseOutput> DeleteAsync(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IResponseOutput<FriendLinkDto>> GetAsync(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IResponseOutput> InsertAsync(CreateFriendLinknput input)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IResponseOutput<PagedList<QueryFriendLinkDto>>> QueryAsync(int page = 1, int limit = 10)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IResponseOutput<List<QueryFriendLinkDto>>> QueryAsync()
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IResponseOutput> UpdateAsync(int id, UpdateFriendLinkInput input)
//        {
//            throw new NotImplementedException();
//        }
//    }
//    #endregion
//}
