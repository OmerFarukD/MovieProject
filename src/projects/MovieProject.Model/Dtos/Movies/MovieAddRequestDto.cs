using Microsoft.AspNetCore.Http;

namespace MovieProject.Model.Dtos.Movies;
public sealed record MovieAddRequestDto
{
    public string? Name { get; init; }

    public string? Description { get; init; }

    public double IMDB { get; init; }

    public DateTime PublishDate { get; init; }

    public IFormFile? Image { get; init;}

    public int CategoryId { get; init; }

    public bool IsActive { get; init; }
    public long DirectorId { get; init; }
}
