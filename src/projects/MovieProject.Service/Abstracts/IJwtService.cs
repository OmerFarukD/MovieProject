using Core.Security.Entities;
using Core.Security.JWT;

namespace MovieProject.Service.Abstracts;

public interface  IJwtService
{
    Task<AccessToken> CreateAccessTokenAsync(User user);
}
