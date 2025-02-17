namespace MovieProject.Model.Dtos.Users;

public sealed class UserUpdateRequestDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string? Username { get; set; } = string.Empty;
}
