using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LandWind.Blog.Core.Response.Base;

namespace LandWind.Blog.Application.Authorize
{
    public interface IAuthorizeService
    {
        /// <summary>
        /// 获取登录地址
        /// </summary>
        /// <returns></returns>
        Task<ResponseResult<string>> GetLoginAddressAsync();

        /// <summary>
        /// 获取AssessToken
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> GetAccessTokenAsync(string code);

        /// <summary>
        /// 登录成功，生成Token
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> GenerateTokenAsync(string accessToken);

    }
}
