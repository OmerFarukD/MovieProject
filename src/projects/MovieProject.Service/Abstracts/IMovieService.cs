using Microsoft.AspNetCore.Http;
using MovieProject.Model.Dtos.Movies;
namespace MovieProject.Service.Abstracts;

public interface IMovieService
{

    // MsSql
    // Redis
    // ElasticSearch
    Task<string> AddAsync(MovieAddRequestDto dto,CancellationToken cancellationToken=default);


    // MsSql
    // Redis
    // ElasticSearch
    Task UpdateAsync(MovieUpdateRequestDto dto, CancellationToken cancellationToken = default);


    // MsSql
    // Redis
    Task<List<MovieResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);


    // MsSql
    // Redis
    Task<MovieResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);


    // MsSql
    // Redis
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);


    // Elastic
    Task<List<MovieResponseDto>> GetAllByCategoryIdAsync(int id, CancellationToken cancellationToken = default);

    // Elastic
    Task<List<MovieResponseDto>> GetAllByDirectorIdAsync(long id, CancellationToken cancellationToken = default);

    // Elastic
    Task<List<MovieResponseDto>> GetAllByImdbRangeAsync(double min, double max, CancellationToken cancellationToken = default);


    // Elastic
    Task<List<MovieResponseDto>> GetAllByIsActiveAsync(bool active, CancellationToken cancellationToken = default);
}
