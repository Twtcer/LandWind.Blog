using LandWind.Blog.Core.Domain.Entities;
using LandWind.Blog.Domain;
using Volo.Abp.Domain.Repositories;

namespace LandWind.Blog.Core.Domain.Repositories
{
    public interface IPostTagRepository: IRepository<PostTag, int>, IBulkInsert<PostTag>
    {
    }
}
