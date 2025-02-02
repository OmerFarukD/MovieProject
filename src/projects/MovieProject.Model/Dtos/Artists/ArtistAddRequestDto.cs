using Microsoft.AspNetCore.Http;
using MovieProject.Model.Entities;

namespace MovieProject.Model.Dtos.Artists;

public sealed record ArtistAddRequestDto
{
    public string? Name { get; init; }
    public string? Surname { get; init; }
    public IFormFile? Image { get; init; }

    public DateTime BirthDate { get; init; }
}
