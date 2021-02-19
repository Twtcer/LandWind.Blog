using System.Threading.Tasks;
using LandWind.Blog.Application.Contracts.Blog;
using LandWind.Blog.Domain.Shared.Base;

namespace LandWind.Blog.Application.Blog
{
    public interface IBlogService
    {
        Task<ResponseResult<string>> InsertPostAsync(PostDto dto);
        Task<ResponseResult> DeletePostAsync(int id);
        Task<ResponseResult<string>> UpdatePostAsync(int id, PostDto dto);
        Task<ResponseResult<PostDto>> GetPostAsync(int id);
    }
}
