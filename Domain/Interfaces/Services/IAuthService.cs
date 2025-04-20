using Microsoft.AspNetCore.Http;
using Shared.General.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task<GeneralResponse<string>> RegisterAsync(RegisterRequestDto request);
        Task<GeneralResponse<AuthResponseDto>> LoginAsync(LoginRequestDto request, HttpContext httpContext);
        //Task<GeneralResponse<(string accessToken, string refreshToken)>> LoginV2Async(LoginRequest request);
    }
}
