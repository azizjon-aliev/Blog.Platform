using BlogPlatform.Service.Common.EntityTypeConfiguration;
using Identity.API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.API.DataProvider.EntityTypeConfiguration;

public class UserConfiguration : BaseEntityTypeConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Username)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(u => u.Username)
            .IsUnique();

        builder.Property(u => u.Email)
            .HasMaxLength(255)
            .IsRequired();

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.Password)
            .HasMaxLength(255)
            .IsRequired();
    }
}