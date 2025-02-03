using Core.DataAccess.Repositories;
using MovieProject.DataAccess.Contexts;
using MovieProject.DataAccess.Repositories.Abstracts;
using MovieProject.Model.Entities;

namespace MovieProject.DataAccess.Repositories.Concretes;

public sealed class MovieRepository : EfRepositoryBase<Movie, Guid, BaseDbContext>, IMovieRepository
{
    public MovieRepository(BaseDbContext context) : base(context)
    {
    }
}
