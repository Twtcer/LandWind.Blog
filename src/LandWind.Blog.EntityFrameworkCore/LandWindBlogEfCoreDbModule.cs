using LandWind.Blog.Core.Options;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace LandWind.Blog.EntityFrameworkCore
{
    [DependsOn( 
        typeof(AbpEntityFrameworkCoreSqlServerModule),
        typeof(AbpEntityFrameworkCoreMySQLModule),
        typeof(AbpEntityFrameworkCoreSqliteModule),
        typeof(AbpEntityFrameworkCorePostgreSqlModule)
        )]
    public class LandWindBlogEfCoreDbModule : AbpModule
    {
        public AppOptions AppOptions;
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            AppOptions  = context.Services.ExecutePreConfiguredActions<AppOptions>();

            context.Services.AddAbpDbContext<LandWindBlogDbContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                var config = context.Services.GetConfiguration(); 
                switch (AppOptions.Storage.EnableDb)
                {
                    case "MySql":
                        options.UseMySQL();
                        break;
                    case "SqlServer":
                        options.UseSqlServer();
                        break;
                    case "Sqlite":
                        options.UseSqlite();
                        break;
                    case "PostgreSql":
                        options.UseNpgsql();
                        break;
                    default:
                        options.UseMySQL();
                        break;
                }
            });
        }
    }
}
