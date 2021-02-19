using LandWind.Blog.Application;
using LandWind.Blog.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace LandWind.Blog.HttpApi.Controllers
{
    /// <summary>
    /// HelloWorld 测试接口
    /// </summary>
    [ApiExplorerSettings(GroupName = Grouping.GroupName_v3)]
    public class HelloWorldController : BaseController
    {
        private readonly IHelloWorldService _helloWorldService;
        public HelloWorldController(IHelloWorldService helloWorldService)
        {
            _helloWorldService = helloWorldService;
        }

        [HttpGet]
        public string HelloWorld()
        {
            return _helloWorldService.HelloWorld();
        }
    }
}
