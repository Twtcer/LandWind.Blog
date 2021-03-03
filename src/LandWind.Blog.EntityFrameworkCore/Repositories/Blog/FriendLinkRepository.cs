using System.Collections.Generic;
using System.Threading.Tasks;
using LandWind.Blog.Core.Domain.Entities; 
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using LandWind.Blog.Core.Domain.Repositories;
using Volo.Abp.EntityFrameworkCore;

namespace LandWind.Blog.EntityFrameworkCore.Repositories.Blog
{
    public class FriendLinkRepository : EfCoreRepository<LandWindBlogDbContext, FriendLink, int>, IFriendLinkRepository
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
