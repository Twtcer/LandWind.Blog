using LandWind.Blog.Domain.Configurations;
using LandWind.Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace LandWind.Blog.EntityFrameworkCore
{ 
    public class LandWindBlogDbContext : AbpDbContext<LandWindBlogDbContext>
    {
        public LandWindBlogDbContext(DbContextOptions<LandWindBlogDbContext> options):base(options)
        {  
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<FriendLink> FriendLinks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
             
            modelBuilder.Configue();
        }
    }
}
