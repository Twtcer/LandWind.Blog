using System;
using LandWind.Blog.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace LandWind.Blog.Domain.Repositories.Interface
{
    public interface IHotNewsRepository : IRepository<HotNews, Guid>, IBulkInsert<HotNews>
    {
    }
}
