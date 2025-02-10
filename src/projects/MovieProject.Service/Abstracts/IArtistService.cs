using MovieProject.Model.Dtos.Artists;

namespace MovieProject.Service.Abstracts;

public interface IArtistService
{
    Task<string> AddAsync(ArtistAddRequestDto dto,CancellationToken cancellation=default);
    Task<string> UpdateAsync(ArtistUpdateRequestDto dto,CancellationToken cancellation=default);

    Task<string> DeleteAsync(long id, CancellationToken cancellation = default);

    Task<ArtistResponseDto> GetByIdAsync(long id, CancellationToken cancellation = default);

    Task<List<ArtistResponseDto>> GetAllAsync(CancellationToken cancellation = default);

    Task<List<ArtistResponseDto>> GetAllByMovieIdAsync(Guid movieId, CancellationToken cancellation = default);
}
