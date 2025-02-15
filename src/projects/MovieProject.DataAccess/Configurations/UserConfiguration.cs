using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
 

namespace MovieProject.DataAccess.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{ 
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users").HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("UserId").IsRequired();
        builder.Property(x => x.CreatedTime).HasColumnName("Created").IsRequired();

        builder.HasMany(x => x.UserRoles);

        builder.Navigation(x => x.UserRoles).AutoInclude();
    }
}
