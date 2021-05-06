using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Configs
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.Property(i => i.Id).UseIdentityColumn().IsRequired();
            builder.Property(ci => ci.ApplicationUserId).IsRequired();
            builder.Property(ci => ci.AgeGroup).IsRequired();
            builder.Property(ci => ci.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(ci => ci.LastName).HasMaxLength(50).IsRequired();
            builder.Property(ci => ci.Username).HasMaxLength(50).IsRequired();
        }
    }
}
