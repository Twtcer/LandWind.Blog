using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using LandWind.Blog.Application.Blog;
using LandWind.Blog.Core.Dto.Blog;
using LandWind.Blog.Core.Response.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LandWind.Blog.HttpApi.Controllers
{
    /// <summary>
    /// 博客文章接口
    /// </summary>
    [ApiExplorerSettings(GroupName = Grouping.GroupName_v1)]
    public class BlogPostController : BaseController
    {
        private readonly IBlogPostService _blogService;
        public BlogPostController(IBlogPostService blogService)
        {
            _blogService = blogService;
        }

        /// <summary>
        /// 添加博客
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ResponseResult<string>> InsertPostAsync([FromBody] PostDto dto)
        {
            return await _blogService.InsertAsync(dto);
        }

        /// <summary>
        /// 删除博客
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public async Task<ResponseResult> DeletePostAsync([Required] int id)
        {
            return await _blogService.DeleteAsync(id);
        }

        /// <summary>
        /// 更新博客
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<ResponseResult<string>> UpdatePostAsync([Required] int id, [FromBody] PostDto dto)
        {
            return await _blogService.UpdateAsync(id, dto);
        }

        /// <summary>
        /// 获取博客
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseResult<PostDto>> GetPostAsync([Required] int id)
        {
            return await _blogService.GetAsync(id);
        }
    }
}
