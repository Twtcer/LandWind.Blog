﻿using System;
using System.Threading.Tasks;
using LandWind.Blog.Core.Response.Base;

namespace LandWind.Blog.Application.Caching.Authorize
{
    public interface IAuthorizeCacheService
    {
        /// <summary>
        /// 获取登录地址(GitHub)
        /// </summary>
        /// <returns></returns>
        Task<ResponseResult<string>> GetLoginAddressAsync(Func<Task<ResponseResult<string>>> factory);

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="code"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> GetAccessTokenAsync(string code, Func<Task<ResponseResult<string>>> factory);

        /// <summary>
        /// 登录成功，生成Token
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> GenerateTokenAsync(string access_token, Func<Task<ResponseResult<string>>> factory);
    }
}