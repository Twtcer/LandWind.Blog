using System.Threading.Tasks;
using LandWind.Blog.Core.Domain.Users.Repositories;

namespace LandWind.Blog.Application.DataSeed
{
    public class UserDataSeedService: SeedServiceBase
    {
        private readonly IUserRepository _users;
        public UserDataSeedService(IUserRepository users)
        {
            _users = users;
        }

        public  async override Task SeedAsync()
        {
          await  base.SeedAsync(_users, "users.json", "RECORDS");
        }
    }
}
