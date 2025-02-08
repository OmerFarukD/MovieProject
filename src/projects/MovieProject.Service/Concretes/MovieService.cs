using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MovieProject.DataAccess.Repositories.Abstracts;
using MovieProject.Model.Dtos.Movies;
using MovieProject.Model.Entities;
using MovieProject.Service.Abstracts;
using MovieProject.Service.Helpers;

namespace MovieProject.Service.Concretes;

public sealed class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;
    private readonly ICloudinaryHelper _cloudinaryHelper;

    public MovieService(IMovieRepository movieRepository, IMapper mapper, ICloudinaryHelper cloudinaryHelper)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
        _cloudinaryHelper = cloudinaryHelper;
    }

    public string Add(MovieAddRequestDto dto)
    {
    
        Movie movie = _mapper.Map<Movie>(dto);
        string url = _cloudinaryHelper.UploadImage(dto.Image, "Movie-project");
         movie.ImageUrl = url;
        _movieRepository.Add(movie);

        return movie.ImageUrl;

    }

    public void Delete(Guid id)
    {
        var movie = _movieRepository.Get(x=>x.Id == id,enableTracking:false);
        if(movie is null)
        {
            // exception fırlat
        }

        _movieRepository.Delete(movie!);
    }

    

    public List<MovieResponseDto> GetAll()
    {
        //var movies = _movieRepository.Query()
        //    .Include(x => x.Category)
        //    .Include(x => x.Director)
        //    .ToList();


        var movies = _movieRepository.GetAll();
        var response = _mapper.Map<List<MovieResponseDto>>(movies);
        return response;
    }

    public List<MovieResponseDto> GetAllByCategoryId(int id)
    {
        var movies = _movieRepository.GetAll(filter: x=>x.CategoryId==id, enableTracking:false);
        var responses = _mapper.Map<List<MovieResponseDto>>(movies);
        return responses;
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
