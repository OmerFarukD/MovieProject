using Core.Security.Entities;
using Core.Security.JWT;
using Microsoft.EntityFrameworkCore;
using MovieProject.DataAccess.Repositories.Abstracts;
using MovieProject.Service.Abstracts;

namespace MovieProject.Service.Concretes;

public sealed class JwtService : IJwtService
{
    private ITokenHelper tokenHelper;
    private IUserRoleRepository _userRoleRepository;

    public JwtService(ITokenHelper tokenHelper, IUserRoleRepository userRoleRepository)
    {
        this.tokenHelper = tokenHelper;
        _userRoleRepository = userRoleRepository;
    }

    public async Task<AccessToken> CreateAccessTokenAsync(User user)
    {
        List<Role> roles = await _userRoleRepository.Query()
            .AsNoTracking()
            .Where(u => u.UserId == user.Id)
            .Select(r => new Role { Id = r.RoleId, Name = r.Role.Name })
            .ToListAsync();

        AccessToken accessToken = tokenHelper.CreateToken(user,roles);
        return accessToken;
    }
}
