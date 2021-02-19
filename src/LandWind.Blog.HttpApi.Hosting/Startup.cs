using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LandWind.Blog.HttpApi.Hosting
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) => services.AddApplication<LandWindBlogHttpApiHostingModule>(); 

        public void Configure(IApplicationBuilder app) => app.InitializeApplication();

    }
}
