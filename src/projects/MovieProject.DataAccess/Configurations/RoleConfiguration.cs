using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MovieProject.DataAccess.Configurations;

public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles").HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("RoleId").IsRequired();
        builder.Property(x => x.CreatedTime).HasColumnName("Created").IsRequired();

        builder.HasMany(x => x.UserRoles);

        builder.Navigation(x => x.UserRoles).AutoInclude();
    }
}
