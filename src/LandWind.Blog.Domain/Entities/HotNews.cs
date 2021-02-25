using System;
using Volo.Abp.Domain.Entities;

namespace LandWind.Blog.Domain.Entities
{
    /// <summary>
    /// 热点新闻
    /// </summary>
    public class HotNews: Entity<Guid>
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public int SourceId { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
