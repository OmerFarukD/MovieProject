using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieProject.Model.Entities;
namespace MovieProject.DataAccess.Configurations;

public sealed class MovieArtistConfiguration : IEntityTypeConfiguration<MovieArtist>
{
    public void Configure(EntityTypeBuilder<MovieArtist> builder)
    {
        builder.ToTable("MovieArtists").HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("MovieArtistId").IsRequired();
        builder.Property(x => x.CreatedTime).HasColumnName("Created").IsRequired();

        builder.HasOne(x => x.Artist).WithMany(x => x.MovieArtists)
            .HasForeignKey(z => z.ArtistId);

        builder.HasOne(x => x.Movie).WithMany(y => y.MovieArtists)
            .HasForeignKey(z => z.MovieId);
    }
}
