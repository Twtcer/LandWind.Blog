using System.Threading.Tasks;
using LandWind.Blog.Application.Users;
using LandWind.Blog.Core.DataAnnotation.Output;
using LandWind.Blog.Core.Dto.Authorize;

namespace LandWind.Blog.Application.Authorize
{
    public interface IAuthorizeService
    {
        /// <summary>
        /// 获取授权地址
        /// </summary>
        /// <returns></returns>
        Task<IResponseOutput> GetAuthorizeUrlAsync(string type);

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<IResponseOutput> GenerateTokenAsync(string code);

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="type"></param>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        Task<IResponseOutput> GenerateTokenAsync(string type, string code, string state); 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IResponseOutput> GenerateTokenAsync(IUserService userService, AccountInput input);

        /// <summary>
        /// 发送授权码
        /// </summary>
        /// <returns></returns>
        Task<IResponseOutput> SendAuthorizeCodeAsync();
    }
}
