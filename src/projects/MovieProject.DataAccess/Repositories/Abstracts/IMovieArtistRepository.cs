using Core.DataAccess.Repositories;
using MovieProject.Model.Entities;

namespace MovieProject.DataAccess.Repositories.Abstracts;

public interface IMovieArtistRepository : IRepository<MovieArtist, long>, IAsyncRepository<MovieArtist,long>
{

}
