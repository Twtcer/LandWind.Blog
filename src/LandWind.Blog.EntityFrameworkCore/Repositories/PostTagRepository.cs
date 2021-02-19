﻿using System.Collections.Generic;
using System.Threading.Tasks;
using LandWind.Blog.Domain.Entities;
using LandWind.Blog.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
namespace LandWind.Blog.EntityFrameworkCore.Repositories
{
    public class PostTagRepository : EfCoreRepository<LandWindBlogDbContext, PostTag, int>, IPostTagRepository
    {
        public PostTagRepository(IDbContextProvider<LandWindBlogDbContext> dbContextProvider) : base(dbContextProvider)
        {
            
        }

        public async Task BulkInsertAsync(IEnumerable<PostTag> ts)
        {
            var context = await GetDbContextAsync();
            await context.Set<PostTag>().AddRangeAsync(ts);
            await context.SaveChangesAsync();
        }
    }
}
