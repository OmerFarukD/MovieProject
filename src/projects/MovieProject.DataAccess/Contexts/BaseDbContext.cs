using Microsoft.EntityFrameworkCore;
using MovieProject.Model.Entities;

namespace MovieProject.DataAccess.Contexts;

public sealed class BaseDbContext : DbContext
{



    public DbSet<Artist> Artists { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<MovieArtist> MovieArtists { get; set; }
    public DbSet<Movie> Movies { get; set; }

}
