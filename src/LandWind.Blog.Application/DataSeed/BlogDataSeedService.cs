using System.Threading.Tasks;
using LandWind.Blog.Core.Domain.Repositories;

namespace LandWind.Blog.Application.DataSeed
{
    public class BlogDataSeedService : SeedServiceBase
    {
        private readonly ICategoryRepository _categories;
        private readonly ITagRepository _tags;
        private readonly IPostRepository _posts;

        public BlogDataSeedService(ICategoryRepository categories, ITagRepository tags, IPostRepository posts)
        {
            _categories = categories;
            _posts = posts;
            _tags = tags;
        }

        public async override Task SeedAsync()
        {
            await base.SeedAsync(_categories, "categories.json");
            await base.SeedAsync(_posts, "posts.json");
            await base.SeedAsync(_tags, "tags.json");
        }
    }
}
