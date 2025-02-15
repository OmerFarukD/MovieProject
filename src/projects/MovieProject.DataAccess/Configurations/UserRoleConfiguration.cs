using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MovieProject.DataAccess.Configurations;

public sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRoles").HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("Id").IsRequired();
        builder.Property(x => x.CreatedTime).HasColumnName("Created").IsRequired();



        builder.HasOne(x => x.User).WithMany(u => u.UserRoles).HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Role).WithMany(u => u.UserRoles).HasForeignKey(x => x.RoleId);


        builder.Navigation(x => x.Role).AutoInclude();
        builder.Navigation(x => x.User).AutoInclude();
    }
}
