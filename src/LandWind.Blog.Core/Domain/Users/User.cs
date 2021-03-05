using System;

namespace LandWind.Blog.Core.Domain.Users
{
    /// <summary>
    /// User
    /// </summary>
    public class User : EntityBase
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public string Identity { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
