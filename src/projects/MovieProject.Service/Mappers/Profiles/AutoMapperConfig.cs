using AutoMapper;
using Core.Security.Entities;
using MovieProject.Model.Dtos.Artists;
using MovieProject.Model.Dtos.Categories;
using MovieProject.Model.Dtos.Movies;
using MovieProject.Model.Dtos.Users;
using MovieProject.Model.Entities;

namespace MovieProject.Service.Mappers.Profiles;

public sealed class AutoMapperConfig : Profile
{

    public AutoMapperConfig()
    {
        CreateMap<CategoryAddRequestDto,Category>();
        CreateMap<CategoryUpdateRequestDto, Category>();
        CreateMap<Category, CategoryResponseDto>();


        CreateMap<MovieUpdateRequestDto,Movie>();
        CreateMap<MovieAddRequestDto, Movie>();
        CreateMap<Movie, MovieResponseDto>();


        CreateMap<Artist, ArtistResponseDto>();
        CreateMap<ArtistAddRequestDto, Artist>();
        CreateMap<ArtistUpdateRequestDto, Artist>();


        CreateMap<User, UserResponseDto>();
    }
}
