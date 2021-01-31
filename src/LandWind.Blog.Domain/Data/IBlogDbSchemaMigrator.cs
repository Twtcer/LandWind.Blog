using System.Threading.Tasks;

namespace LandWind.Blog.Data
{
    public interface IBlogDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
