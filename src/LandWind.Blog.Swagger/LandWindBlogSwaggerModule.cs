//using LandWind.Blog.Domain;
//using Microsoft.AspNetCore.Builder;
//using Volo.Abp;
//using Volo.Abp.Modularity;

//namespace LandWind.Blog.Swagger
//{
//    [DependsOn(
//        typeof(LandWindBlogDomainModule)
//        )]
//    public class LandWindBlogSwaggerModule : AbpModule
//    {
//        public override void ConfigureServices(ServiceConfigurationContext context)
//        {
//            context.Services.AddSwagger();
//        }

//        public override void OnApplicationInitialization(ApplicationInitializationContext context)
//        {
//            context.GetApplicationBuilder().UseSwagger().UseSwaggerUI();
//        }
//    }
//}
