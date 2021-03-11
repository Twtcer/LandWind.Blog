using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandWind.Blog.Core.Dto.Blog
{
    public class FriendLinkDto
    {
        public string Name { get; set; }

        public string Url { get; set; }
    }

    public class QueryFriendLinkDto : TagDto
    {
        public int Total { get; set; }
    }

    public class CreateFriendLinknput : FriendLinkDto
    {

    }

    public class UpdateFriendLinkInput : CreateFriendLinknput
    {

    }
}
