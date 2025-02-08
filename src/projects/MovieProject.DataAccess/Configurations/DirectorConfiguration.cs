using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieProject.Model.Entities;

namespace MovieProject.DataAccess.Configurations;
public sealed class DirectorConfiguration : IEntityTypeConfiguration<Director>
{
    public void Configure(EntityTypeBuilder<Director> builder)
    {
        builder.ToTable("Directors").HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("DirectorId").IsRequired();
        builder.Property(x => x.CreatedTime).HasColumnName("Created").IsRequired();



        builder.HasMany(x => x.Movies).WithOne(x => x.Director).HasForeignKey(x=>x.DirectorId);



        builder.HasData(
            new Director
            {
                Id = 1,
                BirthDay = DateTime.Parse("1959-02-08 16:39:25.0000000"),
                CreatedTime = DateTime.UtcNow,
                ImageUrl = "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcT-r2zKe07oWLgn-Sy993RYt7xe27X4_P_KP82X2Mlrs-EfZlr6_rbSnOkm_QIhRu3Zy7jukIkHAoYTexSo4OJU2g",
                Name="Nuri Bilge",
                Surname = "Ceylan"
            }
            );
    }
}
