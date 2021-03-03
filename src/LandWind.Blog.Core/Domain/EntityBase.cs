using Volo.Abp.Domain.Entities;

namespace LandWind.Blog.Core.Domain
{
    public class EntityBase:Entity<int>
    {
    }

    public class EntityBase<T> : Entity<T>
    {

    } 
}
