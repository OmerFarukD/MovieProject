namespace MovieProject.Model.Dtos.MovieArtists;

public sealed record MovieArtistUpdateRequestDto
{
    public long Id { get; init; }
    public long ArtistId { get; init; }
    public Guid MovieId { get; init; }
}
