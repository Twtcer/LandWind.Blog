using System.Collections.Generic;
using System.Threading.Tasks;
using LandWind.Blog.Domain.Entities;
using LandWind.Blog.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace LandWind.Blog.EntityFrameworkCore.Repositories
{
    public class FriendLinkRepository : EfCoreRepository<LandWindBlogDbContext, FriendLink, int>, IFriendLinkRespository
    {
        public FriendLinkRepository(IDbContextProvider<LandWindBlogDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task BulkInsertAsync(IEnumerable<FriendLink> ts)
        {
            var context = await GetDbContextAsync();
            await context.Set<FriendLink>().AddRangeAsync(ts);
            await context.SaveChangesAsync();
        }
    }
}
