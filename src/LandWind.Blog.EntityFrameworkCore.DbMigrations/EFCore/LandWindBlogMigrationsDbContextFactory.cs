﻿using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LandWind.Blog.EntityFrameworkCore.DbMigrations
{
    public class LandWindBlogMigrationsDbContextFactory : IDesignTimeDbContextFactory<LandWindBlogMigrationsDbContext>
    {
        public LandWindBlogMigrationsDbContext CreateDbContext(string[] args)
        {
           var config = BuildConfiguration();
            //var connStr = config.GetConnectionString("SqlServer");
            //Console.WriteLine($"connect str : {connStr}");
            var builder = new DbContextOptionsBuilder<LandWindBlogMigrationsDbContext>()
                   .UseSqlServer(config.GetConnectionString("SqlServer"));
             
            return new LandWindBlogMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            //Tips:此处读取目录为运行项目根目录
            Console.WriteLine($"dir path : {Directory.GetCurrentDirectory()}");

            return builder.Build();
        } 
       
    }
}
