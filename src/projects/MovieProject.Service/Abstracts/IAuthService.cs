using Core.Security.JWT;
using MovieProject.Model.Dtos.Users;

namespace MovieProject.Service.Abstracts;

public interface IAuthService
{

    Task<AccessToken> RegisterAsync(RegisterRequestDto requestDto,CancellationToken cancellationToken=default);
    Task<AccessToken> LoginAsync(LoginRequestDto requestDto,CancellationToken cancellationToken = default);
}
