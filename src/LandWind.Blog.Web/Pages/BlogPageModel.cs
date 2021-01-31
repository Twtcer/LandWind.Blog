﻿using LandWind.Blog.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace LandWind.Blog.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class BlogPageModel : AbpPageModel
    {
        protected BlogPageModel()
        {
            LocalizationResourceType = typeof(BlogResource);
        }
    }
}