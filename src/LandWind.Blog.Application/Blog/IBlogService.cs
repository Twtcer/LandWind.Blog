using System.Threading.Tasks;
using LandWind.Blog.Core.Dto.Blog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LandWind.Blog.Core.DataAnnotation.Output;

namespace LandWind.Blog.Application.Blog
{
    #region IBlogService
    public interface IBlogService<QueryDtoT, CreateInputT, UpdateInputT, DtoT> where DtoT : class
    { 
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        Task<IResponseOutput> QueryAsync([Range(1, 100)] int page=1, [Range(1, 100)] int limit=10);

        /// <summary>
        /// 查询列表
        /// </summary> 
        /// <returns></returns>
        Task<IResponseOutput> QueryAsync();

        /// <summary>
        /// 获取单个模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResponseOutput<DtoT>> GetAsync(int id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IResponseOutput> InsertAsync(CreateInputT input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResponseOutput> DeleteAsync(int id);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IResponseOutput> UpdateAsync(int id, UpdateInputT input);
    }

    /// <summary>
    /// 报表接口
    /// </summary>
    public interface IStatisticsService
    {
        Task<IResponseOutput<Tuple<int, int, int>>> GetStatisticsAsync();
    }
    #endregion

    #region IBlogObjectService 
    public interface IBlogPostService : IBlogService<QueryPostDto, CreatePostInput, UpdatePostInput, PostDto>
    {
        Task<IResponseOutput> GetByUrlAsync(string url);  
        Task<IResponseOutput> GetPostsByCategoryAsync(string category); 
        Task<IResponseOutput> GetPostsByTagAsync(string tag);
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
