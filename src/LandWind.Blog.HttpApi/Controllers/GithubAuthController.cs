using System.Threading.Tasks;
using LandWind.Blog.Application.Authorize;
using LandWind.Blog.Domain.Shared;
using LandWind.Blog.Domain.Shared.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LandWind.Blog.HttpApi.Controllers
{
    /// <summary>
    /// 授权控制器
    /// </summary>
    [AllowAnonymous]
    [ApiExplorerSettings(GroupName = Grouping.GroupName_v4)]
    public class GithubAuthController : BaseController
    {
        private readonly IAuthorizeService _authorizeService;

        public GithubAuthController(IAuthorizeService authorizeService)
        {
            _authorizeService = authorizeService;
        }

        /// <summary>
        /// 获取登录地址
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("url")]
        public async Task<ResponseResult<string>> GetLoginAddressAsync()
        {
            return await _authorizeService.GetLoginAddressAsync();
        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("access_token")]
        public async Task<ResponseResult<string>> GetAccessTokenAsync(string code)
        {
            return await _authorizeService.GetAccessTokenAsync(code);
        }
    }
}
