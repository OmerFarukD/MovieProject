using AutoMapper;
using Core.Security.Entities;
using MovieProject.Model.Dtos.Artists;
using MovieProject.Model.Dtos.Categories;
using MovieProject.Model.Dtos.Directors;
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
        CreateMap<Movie, MovieDetailDto>().ForMember(x => x.Players, opt =>
            opt.MapFrom(c => c.MovieArtists.Select(x =>$"{x.Artist.Name} {x.Artist.Surname}" ).ToHashSet())
        );


        CreateMap<Artist, ArtistResponseDto>().ForMember(x=>x.FullName, opt=>
            opt.MapFrom(x=> $"{x.Name} {x.Surname}")
            );
        CreateMap<ArtistAddRequestDto, Artist>();
        CreateMap<ArtistUpdateRequestDto, Artist>();


        CreateMap<User, UserResponseDto>().ReverseMap();
        CreateMap<RegisterRequestDto, User>();
        CreateMap<UserUpdateRequestDto, User>();



        CreateMap<Director, DirectorResponseDto>();
        CreateMap<DirectorAddRequestDto,Director>();
        CreateMap<DirectorUpdateRequestDto, Director>();
    }
}
