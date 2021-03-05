using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LandWind.Blog.Application.Users;
using LandWind.Blog.Core.Dto.Authorize;
using LandWind.Blog.Core.Response.Base;

namespace LandWind.Blog.Application.Authorize
{
    public interface IAuthorizeService
    {
        /// <summary>
        /// 获取授权地址
        /// </summary>
        /// <returns></returns>
        Task<ResponseResult<string>> GetAuthorizeUrlAsync(string type);

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> GenerateTokenAsync(string code);

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="type"></param>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> GenerateTokenAsync(string type, string code, string state); 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> GenerateTokenAsync(IUserService userService, AccountInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<ResponseResult> SendAuthorizeCodeAsync();
    }
}
