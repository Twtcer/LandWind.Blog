using System;
using LandWind.Blog.Core.Domain.Entities; 
using Volo.Abp.Domain.Repositories;

namespace LandWind.Blog.Core.Domain.Repositories
{
    public interface IHotNewsRepository : IRepositoryBase<HotNews, Guid>, IBulkInsert<HotNews>
    {
    }
}
