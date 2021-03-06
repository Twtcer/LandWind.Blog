using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace LandWind.Blog.EntityFrameworkCore.DbMigrations
{
    [DependsOn(
        typeof(LandWindBlogEfCoreDbModule)
        )]
    public class LandWindBlogEFCoreDbMigrationsModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<LandWindBlogMigrationsDbContext>();
        }
    }
}
