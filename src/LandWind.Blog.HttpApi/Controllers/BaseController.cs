using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace LandWind.Blog.HttpApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController:AbpController
    {
    }
}
