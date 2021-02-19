using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LandWind.Blog.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace LandWind.Blog.Domain.Repositories
{
    public interface IPostTagRepository: IRepository<PostTag, int>, IBulkInsert<PostTag>
    {
    }
}
