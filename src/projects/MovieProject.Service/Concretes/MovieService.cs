using AutoMapper;
using Core.CrossCuttingConcerns.Cache.Redis;
using MovieProject.DataAccess.Repositories.Abstracts;
using MovieProject.Model.Dtos.Movies;
using MovieProject.Model.Entities;
using MovieProject.Service.Abstracts;
using MovieProject.Service.BusinessRules.Movies;
using MovieProject.Service.Constants.Movies;
using MovieProject.Service.Helpers;

namespace MovieProject.Service.Concretes;

// Movies['movie_list','movie_id']
public sealed class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;
    private readonly ICloudinaryHelper _cloudinaryHelper;
    private readonly MovieBusinessRules _businessRules;
    private readonly IRedisCacheService _cache;

    public MovieService(IMovieRepository movieRepository, IMapper mapper, ICloudinaryHelper cloudinaryHelper, MovieBusinessRules businessRules, IRedisCacheService cache)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
        _cloudinaryHelper = cloudinaryHelper;
        _businessRules = businessRules;
        _cache = cache;
    }

    public async Task<string> AddAsync(MovieAddRequestDto dto, CancellationToken cancellationToken = default)
    {
        await _cache.RemoveDataAsync(RedisMovieKey.MovieListKey);


        await _businessRules.MovieNameMutBeUniqueAsync(dto.Name);

        Movie movie = _mapper.Map<Movie>(dto);

        string url = await _cloudinaryHelper.UploadImageAsync(dto.Image,"movie-store");

        movie.ImageUrl = url;



        await _movieRepository.AddAsync(movie,cancellationToken);


        return MovieMessages.MovieAddedMessage;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {

        await _cache.RemoveDataAsync(RedisMovieKey.MovieListKey);

        await _cache.RemoveDataAsync(RedisMovieKey.GetByIdKey(id));

        await _businessRules.MovieIsPresentAsync(id);

        Movie movie = await _movieRepository.GetAsync(filter:x=>x.Id==id,include:false,cancellationToken:cancellationToken);

        await _movieRepository.DeleteAsync(movie,cancellationToken);

    }

    public async Task<List<MovieResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var cachedMovies = await _cache.GetDataAsync<List<MovieResponseDto>>(RedisMovieKey.MovieListKey);

        if(cachedMovies != null)
        {
            return cachedMovies;
        }



        List<Movie> movies = await _movieRepository
            .GetAllAsync(enableTracking: false, cancellationToken: cancellationToken);

        var movieResponseDtos = _mapper.Map<List<MovieResponseDto>>(movies);

        await _cache.SetDataAsync(RedisMovieKey.MovieListKey,movieResponseDtos);

        return movieResponseDtos;
    }

    public async Task<List<MovieResponseDto>> GetAllByCategoryIdAsync(int id, CancellationToken cancellationToken = default)
    {
        List<Movie> movies = await _movieRepository
             .GetAllAsync(filter:x=>x.CategoryId==id, enableTracking: false, cancellationToken: cancellationToken);

        var movieResponseDtos = _mapper.Map<List<MovieResponseDto>>(movies);

        return movieResponseDtos;
    }

    public async Task<List<MovieResponseDto>> GetAllByDirectorIdAsync(long id, CancellationToken cancellationToken = default)
    {
        List<Movie> movies = await _movieRepository
           .GetAllAsync(filter: x => x.DirectorId == id, enableTracking: false, cancellationToken: cancellationToken);

        var movieResponseDtos = _mapper.Map<List<MovieResponseDto>>(movies);

        return movieResponseDtos;
    }

    public async Task<List<MovieResponseDto>> GetAllByImdbRangeAsync(double min, double max, CancellationToken cancellationToken = default)
    {
        List<Movie> movies = await _movieRepository
          .GetAllAsync(filter: x => x.IMDB<=max && x.IMDB>=min, enableTracking: false, cancellationToken: cancellationToken);

        var movieResponseDtos = _mapper.Map<List<MovieResponseDto>>(movies);

        return movieResponseDtos;
    }

    public async Task<List<MovieResponseDto>> GetAllByIsActiveAsync(bool active, CancellationToken cancellationToken = default)
    {
        List<Movie> movies = await _movieRepository
            .GetAllAsync(filter: x => x.IsActive==active, enableTracking: false, cancellationToken: cancellationToken);

        var movieResponseDtos = _mapper.Map<List<MovieResponseDto>>(movies);

        return movieResponseDtos;
    }

    public async Task<MovieDetailDto> GetDetailAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _businessRules.MovieIsPresentAsync(id);

        Movie movie = await _movieRepository.GetAsync(filter: x=> x.Id==id, enableTracking:false, cancellationToken: cancellationToken);

        MovieDetailDto dto = _mapper.Map<MovieDetailDto>(movie);

        return dto;

    }

    public async Task<MovieResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        string cacheKey = RedisMovieKey.GetByIdKey(id);

        var cahcedData = await _cache.GetDataAsync<MovieResponseDto>(cacheKey);

        if(cahcedData != null)
        {
            return cahcedData;
        }


        await _businessRules.MovieIsPresentAsync(id);

        Movie movie = await _movieRepository.GetAsync(filter: x=>x.Id==id,enableTracking:false,cancellationToken:cancellationToken);

        MovieResponseDto movieResponseDto = _mapper.Map<MovieResponseDto>(movie);

        await _cache.SetDataAsync<MovieResponseDto>(cacheKey,movieResponseDto);

        return movieResponseDto;
    }

    public async Task UpdateAsync(MovieUpdateRequestDto dto, CancellationToken cancellationToken = default)
    {
        await _cache.RemoveDataAsync(RedisMovieKey.MovieListKey);

        await _cache.RemoveDataAsync(RedisMovieKey.GetByIdKey(dto.Id));

        await _businessRules.MovieIsPresentAsync(dto.Id);
        // Movie movie = await _movieRepository.GetAsync(filter: x => x.Id == dto.Id, include: false, cancellationToken: cancellationToken);

        Movie movie = _mapper.Map<Movie>(dto);

        await _movieRepository.UpdateAsync(movie,cancellationToken);
    }
}
