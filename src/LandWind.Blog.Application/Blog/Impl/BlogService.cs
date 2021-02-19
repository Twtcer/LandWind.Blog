using System;
using System.Threading.Tasks;
using LandWind.Blog.Application.Contracts.Blog;
using LandWind.Blog.Domain.Entities;
using LandWind.Blog.Domain.Repositories;
using LandWind.Blog.Domain.Shared.Base;

namespace LandWind.Blog.Application.Blog.Impl
{
    public class BlogService :  IBlogService
    {
        private readonly IPostRepository _postRepository;
        public BlogService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<ResponseResult> DeletePostAsync(int id)
        {
            await _postRepository.DeleteAsync(id);
            return new ResponseResult();
        }

        public async Task<ResponseResult<PostDto>> GetPostAsync(int id)
        {
            var result = new ResponseResult<PostDto>(); 
        
            var post = await _postRepository.GetAsync(id);
            if (post == null)
            {
                result.IsFailed("文章不存在！");
                return result;
            }

             var dto =   new PostDto { 
                Title  = post.Title,
                Author = post.Author,
                CategoryId = post.CategoryId,
                CreationTime= post.CreationTime,
                Html = post.Html,
                Markdown = post.Markdown,
                Url = post.Url
            };

            result.IsSuccessed(dto);
            return result;
        }

        public async Task<ResponseResult<string>> InsertPostAsync(PostDto dto)
        {
            var result = new ResponseResult<string>();

            try
            {
                var entity = new Post
                {
                    Title = dto.Title,
                    Author = dto.Author,
                    Url = dto.Url,
                    Html = dto.Html,
                    Markdown = dto.Markdown,
                    CategoryId = dto.CategoryId,
                    CreationTime = dto.CreationTime
                };

                var post = await _postRepository.InsertAsync(entity);
                result.IsSuccess("添加成功！");
            }
            catch (Exception ex)
            {
                result.IsFailed($"添加失败！,ex:{ex.InnerException?.Message}");
            }

            return result;
        }

        public async Task<ResponseResult<string>> UpdatePostAsync(int id, PostDto dto)
        {
            var result = new ResponseResult<string>();

            var post = await _postRepository.GetAsync(id);
            if(post==null)
            {
                result.IsFailed("文章不存在！");
            }

            post.Title = dto.Title;
            post.Author = dto.Author;
            post.Url = dto.Url;
            post.Markdown = dto.Markdown;
            post.Html = dto.Html;
            post.CategoryId = dto.CategoryId;

            var ret = await _postRepository.UpdateAsync(post);

            result.IsSuccess("更新成功！");
            return result;
        }
    }
}
