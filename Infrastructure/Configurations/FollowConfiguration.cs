using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class FollowConfiguration : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(f => f.FollowingId);
            builder.Property(f => f.FollowerId);
            builder.Property(f => f.FollowDate);

            builder.ToTable("Follows");
        }
    }
}
