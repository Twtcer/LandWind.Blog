using System;
using System.Threading.Tasks; 
using LandWind.Blog.Core.Domain.Repositories; 

namespace LandWind.Blog.Application.Blog
{
    /// <summary>
    /// BlogService
    /// </summary>
    public partial class BlogService : LandWindBlogAppServiceBase/*, IBlogService*/
    {
        //private readonly IPostRepository _postRepository;
        //public BlogService(IPostRepository postRepository)
        //{
        //    _postRepository = postRepository;
        //}

        //public async Task<ResponseResult> DeletePostAsync(int id)
        //{
        //    await _postRepository.DeleteAsync(id);
        //    return new ResponseResult();
        //}

        //public async Task<ResponseResult<PostDto>> GetPostAsync(int id)
        //{
        //    var result = new ResponseResult<PostDto>();

        //    var post = await _postRepository.GetAsync(id);
        //    if (post == null)
        //    {
        //        result.IsFailed("文章不存在！");
        //        return result;
        //    } 
        //    var dto = ObjectMapper.Map<Post, PostDto>(post);

        //    result.IsSuccessed(dto);
        //    return result;
        //}

        //public async Task<ResponseResult<string>> InsertPostAsync(PostDto dto)
        //{
        //    var result = new ResponseResult<string>();

        //    try
        //    {
        //        var entity = ObjectMapper.Map<PostDto, Post>(dto);
        //        var post = await _postRepository.InsertAsync(entity);
        //        result.IsSuccess("添加成功！");
        //    }
        //    catch (Exception ex)
        //    {
        //        result.IsFailed($"添加失败！,ex:{ex.InnerException?.Message}");
        //    }

        //    return result;
        //}

        //public async Task<ResponseResult<string>> UpdatePostAsync(int id, PostDto dto)
        //{
        //    var result = new ResponseResult<string>();

        //    var post = await _postRepository.GetAsync(id);
        //    if (post == null)
        //    {
        //        result.IsFailed("文章不存在！");
        //    }

        //    post.Title = dto.Title;
        //    post.Author = dto.Author;
        //    post.Url = dto.Url;
        //    post.Markdown = dto.Markdown;
        //    post.Html = dto.Html;
        //    post.CategoryId = dto.CategoryId;

        //    var ret = await _postRepository.UpdateAsync(post);

        //    result.IsSuccess("更新成功！");
        //    return result;
        //} 

        //private readonly IBlogCacheService _blogCacheService;
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IPostTagRepository _postTagRepository;
        private readonly IFriendLinkRepository _friendLinksRepository;

        public BlogService(
            IPostRepository posts,
            ICategoryRepository categories,
            ITagRepository tags,
            IPostTagRepository postTags,
            IFriendLinkRepository friendLinks
            )
        {
            _categoryRepository = categories;
            _postRepository = posts;
            _tagRepository = tags;
            _postTagRepository = postTags;
            _friendLinksRepository = friendLinks;
        }
    }

    //public class BlogPostService:IBlogPostService
    //{
        
    //}
}
