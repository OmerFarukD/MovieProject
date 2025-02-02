﻿using Microsoft.EntityFrameworkCore;
using MovieProject.Model.Entities;

namespace MovieProject.DataAccess.Contexts;

public sealed class BaseDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"server=(localdb)\MSSQLLocalDB; Database=Movie_Project_db; Trusted_Connection=true");
    }

    public DbSet<Artist> Artists { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<MovieArtist> MovieArtists { get; set; }
    public DbSet<Movie> Movies { get; set; }

}
