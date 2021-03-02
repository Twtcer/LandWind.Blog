using LandWind.Blog.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace LandWind.Blog.Domain.Repositories
{
    public interface IFriendLinkRepository : IRepository<FriendLink,int>, IBulkInsert<FriendLink>
    {

    }
}
