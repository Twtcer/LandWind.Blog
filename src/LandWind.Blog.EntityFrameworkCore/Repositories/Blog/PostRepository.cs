using LandWind.Blog.Core.Domain.Entities;
using LandWind.Blog.Core.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace LandWind.Blog.EntityFrameworkCore.Repositories.Blog
{
    public class PostRepository : EfCoreRepository<LandWindBlogDbContext, Post, int>, IPostRepository
    {
        public PostRepository(IDbContextProvider<LandWindBlogDbContext> dbContextProvider) : base(dbContextProvider)
        {
        } 
    }
}
