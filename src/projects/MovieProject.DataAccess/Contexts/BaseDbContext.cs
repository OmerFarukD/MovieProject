using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using MovieProject.Model.Entities;
using System.Reflection;

namespace MovieProject.DataAccess.Contexts;

public sealed class BaseDbContext : DbContext
{

    public BaseDbContext(DbContextOptions opt): base(opt)
    {
        
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<MovieArtist> MovieArtists { get; set; }
    public DbSet<Movie> Movies { get; set; }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

}
