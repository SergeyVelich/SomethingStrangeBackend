using Lingva.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lingva.DAL.EF.EntityConfigurations
{
    public class PostTagConfiguration : IEntityTypeConfiguration<PostTag>
    {
        public void Configure(EntityTypeBuilder<PostTag> builder)
        {
            builder
                .HasKey(gu => new { gu.PostId, gu.TagId });
            builder
                .HasOne(gu => gu.Post)
                .WithMany(b => b.PostTags)
                .HasForeignKey(gu => gu.PostId);
            builder
                .HasOne(gu => gu.Tag)
                .WithMany(c => c.PostTags)
                .HasForeignKey(gu => gu.TagId);
        }
    }
}
