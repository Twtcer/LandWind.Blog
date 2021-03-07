using System.Collections.Generic;

namespace LandWind.Blog.Core.Options
{
    public class SignatureOptions
    {
        public string Path { get; set; } 
        public Dictionary<string, string> Urls { get; set; } = new Dictionary<string, string>();
    }
}