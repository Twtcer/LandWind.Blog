using LandWind.Blog.Core.Domain.Options;
using LandWind.Blog.Domain;
using LandWind.Blog.Domain.Configurations;
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
    public class LandWindBlogEFCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<LandWindBlogDbContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                var connectStr = Appsettings.ConnectionStrings; 
                switch (Appsettings.EnableDb)
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
