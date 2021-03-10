using System.Threading.Tasks;
using LandWind.Blog.Core.Response.Base;
using LandWind.Blog.Core.Dto.Blog;
using System;
using System.Collections.Generic;

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
        /// 查询列表
        /// </summary> 
        /// <returns></returns>
        Task<ResponseResult<List<QueryDtoT>>> QueryAsync();

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
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResponseResult> UpdateAsync(int id, UpdateInputT input);
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
    public interface IBlogPostService : IBlogService<QueryPostDto, CreatePostInput, UpdatePostInput, PostDto>
    {
        Task<ResponseResult<PostDto>> GetByUrlAsync(string url);
    }

    public interface IBlogCategoryService : IBlogService<QueryCategoryDto, CreateCategoryInput, UpdateCategoryInput, CategoryDto>
    {

    }

    public interface IBlogTagsService : IBlogService<QueryTagDto, CreateTagInput, UpdateTagInput, TagDto>
    {

    }

    public interface IBlogFriendLinkService : IBlogService<QueryFriendLinkDto, CreateFriendLinknput, UpdateFriendLinkInput, FriendLinkDto>
    {

    }

    #endregion
}
