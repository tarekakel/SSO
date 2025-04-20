

using Asp.Versioning;
using Domain.Entities;
using Domain.Interfaces.Services;
using Infrastructure.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Shared.General.Dtos;


[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {


        var result = await _authService.RegisterAsync(request);
        if (!result.Success)
            return BadRequest(result);
        return Ok(result);
    }
    [ApiVersion("1.0")]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var result = await _authService.LoginAsync(request, HttpContext);
        if (!result.Success)
            return Unauthorized(result);
        return Ok(result);

    }
    //[ApiVersion("2.0")]
    //[HttpPost("loginr")]
    //public async Task<IActionResult> Login(LoginRequest request)
    //{
    //    var user = await _userManager.FindByEmailAsync(request.Email);
    //    if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
    //        return Unauthorized("Invalid credentials");

    //    var roles = await _userManager.GetRolesAsync(user);
    //    var (accessToken, refreshToken) = await _tokenService.GenerateTokensWithRefreshAsync(user, roles);

    //    return Ok(new
    //    {
    //        accessToken,
    //        refreshToken
    //    });
    //}

}
