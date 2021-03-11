using System.Collections.Generic;
using System.Threading.Tasks;
using LandWind.Blog.Core.DataAnnotation.Output;
using LandWind.Blog.Core.Domain.Users;
using LandWind.Blog.Core.Dto.Users; 

namespace LandWind.Blog.Application.Users
{
    public interface IUserService
    {
        Task<IResponseOutput> CreateUserAsync(CreateUserInput input);

        Task<IResponseOutput> DeleteUserAsync(string id);

        Task<IResponseOutput> UpdateUserAsync(string id, UpdateUserinput input);

        Task<IResponseOutput> UpdatePasswordAsync(string id, string password);

        Task<IResponseOutput> SettingAdminAsync(string id, bool isAdmin);

        Task<IResponseOutput<List<UserDto>>> GetUsersAsync();

        Task<IResponseOutput<UserDto>> GetUserAsync(string id);

        Task<IResponseOutput<UserDto>> GetCurrentUserAsync();

        Task< User> CreateUserAsync(string username, string type, string identity, string name, string avatar, string email);

        Task<User> VerifyByAccountAsync(string username, string password);

        Task<User> GetDefaultUserAsync();
    }
}
