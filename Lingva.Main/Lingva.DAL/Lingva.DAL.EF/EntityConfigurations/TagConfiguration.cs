using Lingva.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lingva.DAL.EF.EntityConfigurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            //builder
            //    .HasMany(x => x.PostTags)
            //    .WithOne(x => x.Tag)
            //    .HasForeignKey(gu => gu.TagId);
        }
    }
}
