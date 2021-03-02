using System.Threading.Tasks;
using LandWind.Blog.Application.Contracts.Blog;
using LandWind.Blog.Domain.Shared.Base;

namespace LandWind.Blog.Application.Blog
{
    public partial interface IBlogService<DtoT> where DtoT : class
    {
        Task<ResponseResult<string>> InsertPostAsync(DtoT dto);
        Task<ResponseResult> DeletePostAsync(int id);
        Task<ResponseResult<string>> UpdatePostAsync(int id, DtoT dto);
        Task<ResponseResult<DtoT>> GetPostAsync(int id);
    }

    public partial interface IBlogPostService
    {
        
    }

    public partial interface IBlogCategorytService
    {

    }

    public partial interface IBlogTagsService
    {

    }

    public partial interface IBlogFriendLinkService
    {

    }

    public partial interface IBlogAdminService
    {

    }
}
