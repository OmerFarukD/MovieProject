using AutoMapper;
using MovieProject.DataAccess.Repositories.Abstracts;
using MovieProject.Model.Dtos.Movies;
using MovieProject.Service.Abstracts;

namespace MovieProject.Service.Concretes;

public sealed class MovieService : IMovieService
{
    private IMovieRepository _movieRepository;
    private IMapper _mapper;

    public MovieService(IMovieRepository movieRepository, IMapper mapper)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
    }

    public void Add(MovieAddRequestDto dto)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public List<MovieResponseDto> GetAll()
    {
        throw new NotImplementedException();
    }

    public List<MovieResponseDto> GetAllByCategoryId(int id)
    {
        throw new NotImplementedException();
    }

    public List<MovieResponseDto> GetAllByDirectorId(long id)
    {
        throw new NotImplementedException();
    }

    public List<MovieResponseDto> GetAllByImdbRange(double min, double max)
    {
        throw new NotImplementedException();
    }

    public List<MovieResponseDto> GetAllByIsActive(bool active)
    {
        throw new NotImplementedException();
    }

    public MovieResponseDto? GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Update(MovieUpdateRequestDto dto)
    {
        throw new NotImplementedException();
    }
}
