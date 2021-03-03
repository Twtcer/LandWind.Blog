using LandWind.Blog.Core.Domain.Entities;
using LandWind.Blog.Domain; 
using Volo.Abp.Domain.Repositories;

namespace LandWind.Blog.Core.Domain.Repositories
{
    public interface ITagRepository: IRepository<Tag, int>, IBulkInsert<Tag>
    {

    }
}
