
using Domain.Entities;
using Domain.Interfaces.Services;
using Infrastructure.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Shared.General.Dtos;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IUserSessionService _userSessionService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService,
            IUserSessionService userSessionService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userSessionService = userSessionService;
        }

        public async Task<GeneralResponse<string>> RegisterAsync(RegisterRequestDto request)
        {
            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return GeneralResponse<string>.ErrorResponse(errors);
            }

            await _userManager.AddToRoleAsync(user, "User");

            return GeneralResponse<string>.SuccessResponse("User created successfully.");
        }

        public async Task<GeneralResponse<AuthResponseDto>> LoginAsync(LoginRequestDto request, HttpContext httpContext)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return GeneralResponse<AuthResponseDto>.ErrorResponse("User Name Or Password Not Correct!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
                return GeneralResponse<AuthResponseDto>.ErrorResponse("User Name Or Password Not Correct!");

            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenService.GenerateToken(user, roles);

            await _userSessionService.LogUserSessionAsync(user, httpContext);

            return GeneralResponse<AuthResponseDto>.SuccessResponse(new AuthResponseDto(token));
        }

        //public async Task<GeneralResponse<(string accessToken, string refreshToken)>> LoginV2Async(LoginRequest request)
        //{
        //    var user = await _userManager.FindByEmailAsync(request.Email);
        //    if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        //        return GeneralResponse<(string, string)>.ErrorResponse("Invalid credentials");

        //    var roles = await _userManager.GetRolesAsync(user);
        //    var (accessToken, refreshToken) = await _tokenService.GenerateTokensWithRefreshAsync(user, roles);

        //    return GeneralResponse<(string, string)>.SuccessResponse((accessToken, refreshToken));
        //}
    }
}
