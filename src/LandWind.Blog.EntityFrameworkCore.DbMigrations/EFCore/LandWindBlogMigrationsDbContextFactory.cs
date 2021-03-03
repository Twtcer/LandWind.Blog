using LandWind.Blog.Core.Domain.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LandWind.Blog.EntityFrameworkCore.DbMigrations
{
    public class LandWindBlogMigrationsDbContextFactory : IDesignTimeDbContextFactory<LandWindBlogMigrationsDbContext>
    {
        public LandWindBlogMigrationsDbContext CreateDbContext(string[] args)
        {
            //var config = BuildConfiguration();  
            var builder = new DbContextOptionsBuilder<LandWindBlogMigrationsDbContext>();
            var connectStr = Appsettings.ConnectionStrings;
              switch (Appsettings.EnableDb)
            {
                case "MySql":
                    builder.UseMySql(connectStr, ServerVersion.FromString("10.4.12-MariaDB"));
                    break;
                case "SqlServer":
                    builder.UseSqlServer(connectStr);
                    break;
                case "Sqlite":
                    builder.UseSqlite(connectStr);
                    break;
                case "PostgreSql":
                    builder.UseNpgsql(connectStr);
                    break;
                default:
                    builder.UseSqlServer(connectStr);
                    break;
            } 
             
            return new LandWindBlogMigrationsDbContext(builder.Options);
        }

        //private static IConfigurationRoot BuildConfiguration()
        //{
        //    var builder = new ConfigurationBuilder()
        //     .SetBasePath(Directory.GetCurrentDirectory())
        //     .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        //    //Tips:此处读取目录为运行项目根目录
        //    //Console.WriteLine($"dir path : {Directory.GetCurrentDirectory()}");

        //    return builder.Build();
        //} 
       
    }
}
