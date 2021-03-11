using System.Collections.Generic;
using System.Threading.Tasks;
using LandWind.Blog.Core.Domain.Entities;
using LandWind.Blog.Domain; 
using Volo.Abp.Domain.Repositories;

namespace LandWind.Blog.Core.Domain.Repositories
{
    public interface ITagRepository: IRepositoryBase<Tag,int>, IBulkInsert<Tag>
    {
        /// <summary>
        /// Get tag list by names
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        //Task<List<Tag>> GetListAsync(List<string> names);
    }
}
