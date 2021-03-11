using LandWind.Blog.Core.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace LandWind.Blog.Core.Domain.Repositories
{
    public interface IFriendLinkRepository : IRepositoryBase<FriendLink>, IBulkInsert<FriendLink>
    {

    }
}
