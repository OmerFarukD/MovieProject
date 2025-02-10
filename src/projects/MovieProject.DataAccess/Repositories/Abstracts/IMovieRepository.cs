using Core.DataAccess.Repositories;
using MovieProject.Model.Entities;

namespace MovieProject.DataAccess.Repositories.Abstracts;

public interface IMovieRepository : IRepository<Movie, Guid>, IAsyncRepository<Movie,Guid>
{
}
