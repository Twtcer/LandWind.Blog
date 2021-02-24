using AutoMapper;
using LandWind.Blog.Application.Contracts.Blog;
using LandWind.Blog.Domain.Entities;

namespace LandWind.Blog.Application
{
    public class LandWindBlogAutoMapperProfile:Profile
    {
        public LandWindBlogAutoMapperProfile()
        {
            //post,postDto映射
            CreateMap<Post, PostDto>();
            // 忽略Id字段更新
            CreateMap<PostDto, Post>().ForMember(a => a.Id, opt => opt.Ignore());


        }
    }
}
