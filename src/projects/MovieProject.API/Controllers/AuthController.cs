using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Model.Dtos.Users;
using MovieProject.Service.Abstracts;
using System.Security.Claims;

namespace MovieProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto dto)
    {
        var result = await _authService.RegisterAsync(dto);
        return Ok(result);
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto dto)
    {
        var result = await _authService.LoginAsync(dto);
        return Ok(result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update(UserUpdateRequestDto dto)
    {
        var userId =Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

        var result = await _authService.UpdateUserAsync(userId,dto);

        return Ok(result);
    }


    [HttpGet("userinfo")]
    public IActionResult GetCurrentUser()
    {
        var userId = HttpContext.User.Claims.FirstOrDefault(x=> x.Type==ClaimTypes.NameIdentifier).Value;
        var claims = HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Role).ToList();

        var roleNames = claims.Select(x => x.Value).ToList();
        return Ok(new { UserId = userId, Roles = roleNames });
    }
}
