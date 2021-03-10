using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LandWind.Blog.Core.Domain.Entities; 
using Volo.Abp.Domain.Repositories;

namespace LandWind.Blog.Core.Domain.Repositories
{
    public interface IPostRepository: IRepository<Post,int>
    {
        /// <summary>
        /// Get post list by paging.
        /// </summary>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <returns></returns>
        Task<Tuple<int, List<Post>>> GetPagedListAsync(int skipCount, int maxResultCount);

        /// <summary>
        /// Get post list by category.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        Task<List<Post>> GetListByCategoryAsync(string category);

        /// <summary>
        /// Get post list by tag.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        Task<List<Post>> GetListByTagAsync(string tag);

        /// <summary>
        /// Get post count by category id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> GetCountByCategoryAsync(int id);

        /// <summary>
        /// Get post count by tag id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> GetCountByTagAsync(int id);
    }
}
