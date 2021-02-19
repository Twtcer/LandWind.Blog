using LandWind.Blog.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace LandWind.Blog.Domain.Repositories
{
    public interface IFriendLinkRespository:IRepository<FriendLink,int>, IBulkInsert<FriendLink>
    {

    }
}
