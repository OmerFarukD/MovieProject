

namespace MovieProject.Model.Dtos.Directors;

public sealed record DirectorResponseDto
{
    public long Id { get; init; }
    public string? Name { get; init; }
    public string? Surname { get; init; }
    public string ImageUrl { get; init; }

    public DateTime BirthDay { get; init; }
}
