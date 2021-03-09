using System.Threading.Tasks;
using LandWind.Blog.Core.Response.Base;
using LandWind.Blog.Core.Dto.Blog;
using System;

namespace LandWind.Blog.Application.Blog
{
    #region IBlogService
    public interface IBlogService<QueryDtoT, CreateInputT, UpdateInputT, DtoT> where DtoT : class
    {
        /// <summary>
        /// 分页查询 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResponseResult<PagedList<QueryDtoT>>> QueryAsync(PagingInput input);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<ResponseResult<PagedList<QueryDtoT>>> QueryAsync(int page, int limit, Func<Task<ResponseResult<PagedList<QueryDtoT>>>> func);

        /// <summary>
        /// 获取单个模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResponseResult<DtoT>> GetAsync(int id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResponseResult> InsertAsync(CreateInputT input);

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
        Task<ResponseResult<string>> UpdateAsync(int id, UpdateInputT inputT);
    }

    /// <summary>
    /// 报表接口
    /// </summary>
    public interface IStatisticsService
    {
        Task<ResponseResult<Tuple<int, int, int>>> GetStatisticsAsync();
    }
    #endregion

    #region IBlogObjectService 
    public interface IBlogPostService : IBlogService<QueryPostDto, CreatePostInput, UpdatePostInput,PostDto>
    {
        Task<ResponseResult<PostDto>> GetByUrlAsync(string url);
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

    #endregion
}
