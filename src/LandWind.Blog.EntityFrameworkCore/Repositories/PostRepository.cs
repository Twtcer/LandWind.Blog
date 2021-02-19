using LandWind.Blog.Domain.Entities;
using LandWind.Blog.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace LandWind.Blog.EntityFrameworkCore.Repositories
{
    public class PostRepository : EfCoreRepository<LandWindBlogDbContext, Post, int>, IPostRepository
    {
        public PostRepository(IDbContextProvider<LandWindBlogDbContext> dbContextProvider) : base(dbContextProvider)
        {
        } 
    }
}
