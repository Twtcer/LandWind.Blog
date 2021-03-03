using LandWind.Blog.Core.Const;
using LandWind.Blog.Core.Domain.Entities; 
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace LandWind.Blog.EntityFrameworkCore
{
    public static class LandWindDbContextModelCreatingExtensions
    {
        public static void Configue(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<Post>(p =>
            {
                p.ToTable(LandWindBlogDbConsts.DbTablePrefix + DbTableName.Posts);
                p.HasKey(x => x.Id);
                p.Property(x => x.Title).HasMaxLength(200).IsRequired();
                p.Property(x => x.Author).HasMaxLength(10);
                p.Property(x => x.Url).HasMaxLength(100).IsRequired();
                p.Property(x => x.Html).IsRequired();
                p.Property(x => x.Markdown).IsRequired();
                p.Property(x => x.CategoryId).HasColumnType("int");
                p.Property(x => x.CreationTime).HasColumnType("datetime");
            });
            builder.Entity<Category>(b =>
            {
                b.ToTable(LandWindBlogDbConsts.DbTablePrefix + DbTableName.Categories);
                b.HasKey(x => x.Id);
                b.Property(x => x.CategoryName).HasMaxLength(50).IsRequired();
                b.Property(x => x.DisplayName).HasMaxLength(50).IsRequired();
            });

            builder.Entity<Tag>(b =>
            {
                b.ToTable(LandWindBlogDbConsts.DbTablePrefix + DbTableName.Tags);
                b.HasKey(x => x.Id);
                b.Property(x => x.TagName).HasMaxLength(50).IsRequired();
                b.Property(x => x.DisplayName).HasMaxLength(50).IsRequired();
            });

            builder.Entity<PostTag>(b =>
            {
                b.ToTable(LandWindBlogDbConsts.DbTablePrefix + DbTableName.PostTags);
                b.HasKey(x => x.Id);
                b.Property(x => x.PostId).HasColumnType("int").IsRequired();
                b.Property(x => x.TagId).HasColumnType("int").IsRequired();
            });

            builder.Entity<FriendLink>(b =>
            {
                b.ToTable(LandWindBlogDbConsts.DbTablePrefix + DbTableName.Friendlinks);
                b.HasKey(x => x.Id);
                b.Property(x => x.Title).HasMaxLength(20).IsRequired();
                b.Property(x => x.LinkUrl).HasMaxLength(100).IsRequired();
            });

            builder.Entity<HotNews>(a =>
            {
                a.ToTable(LandWindBlogDbConsts.DbTablePrefix + DbTableName.HotNews);
                a.HasKey(a => a.Id);
                a.Property(x => x.Title).HasMaxLength(200).IsRequired(); 
                a.Property(x => x.Url).HasMaxLength(100).IsRequired();
                a.Property(x => x.CreationTime).HasColumnType("datetime");
            });
        }
    }
}
