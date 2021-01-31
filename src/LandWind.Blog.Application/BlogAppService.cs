using System;
using System.Collections.Generic;
using System.Text;
using LandWind.Blog.Localization;
using Volo.Abp.Application.Services;

namespace LandWind.Blog
{
    /* Inherit your application services from this class.
     */
    public abstract class BlogAppService : ApplicationService
    {
        protected BlogAppService()
        {
            LocalizationResource = typeof(BlogResource);
        }
    }
}
