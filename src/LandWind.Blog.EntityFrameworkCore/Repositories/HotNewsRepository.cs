using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LandWind.Blog.Domain.Entities; 
using LandWind.Blog.Domain.Repositories.Interface;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace LandWind.Blog.EntityFrameworkCore.Repositories
{
    public class HotNewsRepository : EfCoreRepository<LandWindBlogDbContext, HotNews, Guid>, IHotNewsRepository
    {
        public HotNewsRepository(IDbContextProvider<LandWindBlogDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task BulkInsertAsync(IEnumerable<HotNews> ts)
        {
            var context = await GetDbContextAsync();
            await context.Set<HotNews>().AddRangeAsync(ts);
            await context.SaveChangesAsync();
        }
    }
}
