namespace MovieProject.Model.Dtos.Artists;

// record - record = olur
// record - class = olamaz

// record - interface = olur 
public sealed record ArtistResponseDto 
{
    public long Id { get; init; }
    public string? ImageUrl { get; init; }

    public DateTime BirthDate { get; init; }
    public string? FullName { get; init; }
}