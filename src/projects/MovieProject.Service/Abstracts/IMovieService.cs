using Microsoft.AspNetCore.Http;
using MovieProject.Model.Dtos.Movies;
namespace MovieProject.Service.Abstracts;

public interface IMovieService
{
    Task<string> AddAsync(MovieAddRequestDto dto,CancellationToken cancellationToken=default);
    
    Task UpdateAsync(MovieUpdateRequestDto dto, CancellationToken cancellationToken = default);

    Task<List<MovieResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<MovieResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<MovieResponseDto>> GetAllByCategoryIdAsync(int id, CancellationToken cancellationToken = default);

    Task<List<MovieResponseDto>> GetAllByDirectorIdAsync(long id, CancellationToken cancellationToken = default);

    Task<List<MovieResponseDto>> GetAllByImdbRangeAsync(double min, double max, CancellationToken cancellationToken = default);

    Task<List<MovieResponseDto>> GetAllByIsActiveAsync(bool active, CancellationToken cancellationToken = default);
}
