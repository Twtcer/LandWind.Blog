using System.Collections.Generic;
using System.Threading.Tasks;
using LandWind.Blog.Core.Domain.Users;
using LandWind.Blog.Core.Dto.Users;
using LandWind.Blog.Core.Response.Base;

namespace LandWind.Blog.Application.Users
{
    public interface IUserService
    {
        Task<ResponseResult> CreateUserAsync(CreateUserInput input);

        Task<ResponseResult> DeleteUserAsync(string id);

        Task<ResponseResult> UpdateUserAsync(string id, UpdateUserinput input);

        Task<ResponseResult> UpdatePasswordAsync(string id, string password);

        Task<ResponseResult> SettingAdminAsync(string id, bool isAdmin);

        Task<ResponseResult<List<UserDto>>> GetUsersAsync();

        Task<ResponseResult<UserDto>> GetUserAsync(string id);

        Task<ResponseResult<UserDto>> GetCurrentUserAsync();

        Task< User> CreateUserAsync(string username, string type, string identity, string name, string avatar, string email);

        Task<User> VerifyByAccountAsync(string username, string password);

        Task<User> GetDefaultUserAsync();
    }
}
