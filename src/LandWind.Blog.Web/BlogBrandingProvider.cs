﻿using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace LandWind.Blog.Web
{
    [Dependency(ReplaceServices = true)]
    public class BlogBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Blog";
    }
}
