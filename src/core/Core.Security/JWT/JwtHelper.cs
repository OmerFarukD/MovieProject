using Core.Security.Encryption;
using Core.Security.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Core.Security.JWT;

public class JwtHelper : ITokenHelper
{

    public IConfiguration Configuration { get;}

    private readonly TokenOptions _tokenOptions;

    private DateTime _accessTokenExpiration;

    public JwtHelper(IConfiguration configuration)
    {
        Configuration = configuration;
        _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
    }

    public AccessToken CreateToken(User user, List<Role> roles)
    {
        _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigninfCredentials(securityKey);
        JwtSecurityToken jwt = CreateJwtSecurityToken(_tokenOptions,user,signingCredentials,roles);
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

        string token = handler.WriteToken(jwt);

        return new AccessToken { Token = token, Expiration = _accessTokenExpiration };
    }


    private JwtSecurityToken CreateJwtSecurityToken(
        TokenOptions tokenOptions,
        User user,
        SigningCredentials signingCredentials,
        List<Role> roles)
    {
        JwtSecurityToken jwt = new JwtSecurityToken(
            tokenOptions.Issuer,
            tokenOptions.Audience,
            expires: _accessTokenExpiration,
            claims:SetClaims(user,roles),
            signingCredentials:signingCredentials
            );

        return jwt;
    }


    private List<Claim> SetClaims(User user, List<Role> roles)
    {
        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()));
        claims.Add(new Claim(ClaimTypes.Email, user.Email));
        claims.Add(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));
        claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role,x.Name)));

        return claims;
    }

}
