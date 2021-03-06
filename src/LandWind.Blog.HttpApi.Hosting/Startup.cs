using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LandWind.Blog.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) => services.AddApplication<LandWindBlogApiModule>(); 

        public void Configure(IApplicationBuilder app) => app.InitializeApplication();
    }
}
