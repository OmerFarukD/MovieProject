using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieProject.Model.Entities;

namespace MovieProject.DataAccess.Configurations;

public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        
        builder.ToTable("Categories_tb").HasKey(x=>x.Id);
        builder.Property(x => x.Id).HasColumnName("CategoryId").IsRequired();
        builder.Property(x => x.CreatedTime).HasColumnName("Created").IsRequired();
        builder.Property(x => x.Name).HasColumnName("CategoryName").IsRequired();
        builder.HasMany(x=>x.Movies);

        builder.HasData(
            new Category { Id = 1, CreatedTime = DateTime.UtcNow, Name = "Korku" },
            new Category { Id=2, CreatedTime = DateTime.UtcNow,Name="Dram"}
            );
    }
}
