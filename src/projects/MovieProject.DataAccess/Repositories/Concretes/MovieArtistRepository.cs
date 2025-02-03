using Core.DataAccess.Repositories;
using MovieProject.DataAccess.Contexts;
using MovieProject.DataAccess.Repositories.Abstracts;
using MovieProject.Model.Entities;

namespace MovieProject.DataAccess.Repositories.Concretes;

public sealed class MovieArtistRepository : EfRepositoryBase<MovieArtist, long, BaseDbContext>, IMovieArtistRepository
{
    public MovieArtistRepository(BaseDbContext context) : base(context)
    {
    }
}
