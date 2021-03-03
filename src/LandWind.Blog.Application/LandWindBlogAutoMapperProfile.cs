using AutoMapper;
using LandWind.Blog.Core.Domain.Entities;
using LandWind.Blog.Core.Dto.Blog;

namespace LandWind.Blog
{
    public class LandWindBlogAutoMapperProfile : Profile
    {
        public LandWindBlogAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            //post,postDto映射
            CreateMap<Post, PostDto>();
            // 忽略Id字段更新
            CreateMap<PostDto, Post>().ForMember(a => a.Id, opt => opt.Ignore());
        }
    }
}
