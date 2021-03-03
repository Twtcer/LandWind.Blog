using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LandWind.Blog.Core.Options;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace LandWind.Blog.Core
{
    [DependsOn(typeof(AbpDddDomainModule))]
    public class LandWindBlogCoreModule:AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            var swagger = new SwaggerOptions();
        }
    }
}
