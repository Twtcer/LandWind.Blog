using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandWind.Blog.Application.HelloWorld
{
    public class HelloWorldService: LandWIndBlogAppServiceBase, IHelloWorldService
    {
        public string HelloWorld()
        {
            return $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")},Hello World!";
        }
    }
}
