using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace LandWind.Blog.EntityFrameworkCore.DbMigrations
{
    public class LandWindBlogMigrationsDbContextFactory : IDesignTimeDbContextFactory<LandWindBlogMigrationsDbContext>
    {
        public LandWindBlogMigrationsDbContext CreateDbContext(string[] args)
        {
           var config = BuildConfiguration();
            var builder = new DbContextOptionsBuilder<LandWindBlogMigrationsDbContext>()
                   //.UseSqlServer(config.GetConnectionString("DefaultConnection"));
                   .UseSqlServer("Data Source=192.168.123.188,4133;User Id=sa;Password=sa123SA!@#;Initial Catalog=LandWindBlogDb;Pooling=true;Min Pool Size=1");
             
            return new LandWindBlogMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        } 
       
    }
}
