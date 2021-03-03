using System;
using Volo.Abp.Domain.Entities;

namespace LandWind.Blog.Core.Domain.Entities
{
    /// <summary>
    /// 热点新闻
    /// </summary>
    public class HotNews: EntityBase<Guid>
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public int SourceId { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
