using System.Collections.Generic;
using System.Threading.Tasks;
using LandWind.Blog.Domain.Entities;
using LandWind.Blog.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace LandWind.Blog.EntityFrameworkCore.Repositories
{
    public class TagRepository : EfCoreRepository<LandWindBlogDbContext, Tag, int>, ITagRepository
    {
        public TagRepository(IDbContextProvider<LandWindBlogDbContext> dbContextProvider) : base(dbContextProvider)
        { 
        }

        public async Task BulkInsertAsync(IEnumerable<Tag> ts)
        {
            var context = await GetDbContextAsync();
            await context.Set<Tag>().AddRangeAsync(ts);
            await context.SaveChangesAsync();
        }
    }
}
