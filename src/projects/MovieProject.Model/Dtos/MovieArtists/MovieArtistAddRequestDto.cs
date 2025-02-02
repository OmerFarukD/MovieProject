namespace MovieProject.Model.Dtos.MovieArtists;

public sealed record MovieArtistAddRequestDto
{
    public long ArtistId { get; init; }
    public Guid MovieId { get; init; }
}
