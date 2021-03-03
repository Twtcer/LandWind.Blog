using System.Threading.Tasks;
using LandWind.Blog.Core.Response.Base; 
using LandWind.Blog.Core.Dto.Blog; 

namespace LandWind.Blog.Application.Blog
{
    public interface IBlogService<DtoT, QueryDto> where DtoT : class where QueryDto:class
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <returns></returns>
        Task<ResponseResult<PagedList<QueryDto>>> QueryAsync(PagingInput input);

        /// <summary>
        /// 获取单个模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResponseResult<DtoT>> GetAsync(int id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> InsertAsync(DtoT dto);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResponseResult> DeleteAsync(int id);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> UpdateAsync(int id, DtoT dto);  
    }

    public interface IBlogPostService : IBlogService<PostDto, QueryPostDto>
    {

    }

    //public partial interface IBlogCategorytService : IBlogService<CategoryDto, QueryCategoryDto>
    //{

    //}

    //public partial interface IBlogTagsService : IBlogService<TagDto, QueryTagDto>
    //{

    //}

    //public partial interface IBlogFriendLinkService : IBlogService<FriendLinkDto, QueryFriendLinkDto>
    //{

    //}

    //public partial interface IBlogAdminService : IBlogService<AdminDto, QueryAdminDto>
    //{

    //}
}
