using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserSessionService : IUserSessionService
    {
        private readonly IUserSessionRepository _repository;
        private readonly ILogger<UserSessionService> _logger;

        public UserSessionService(IUserSessionRepository repository, ILogger<UserSessionService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task LogUserSessionAsync(ApplicationUser user, HttpContext httpContext)
        {
            var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            var userAgent = httpContext.Request.Headers["User-Agent"].ToString();

            var session = new UserSession
            {
                UserId = user.Id,
                IPAddress = ip,
                Browser = userAgent,
                OperatingSystem = ParseOSFromUserAgent(userAgent),
                LoginTime = DateTime.UtcNow
            };

            await _repository.AddAsync(session);
            await _repository.SaveChangesAsync();
        }

        public async Task MarkLogoutAsync(string userId)
        {
            var session = await _repository.GetLatestActiveSessionAsync(userId);
            if (session != null)
            {
                session.LogoutTime = DateTime.UtcNow;
                await _repository.UpdateAsync(session);
                await _repository.SaveChangesAsync();
            }
        }

        private string ParseOSFromUserAgent(string userAgent)
        {
            if (userAgent.Contains("Windows")) return "Windows";
            if (userAgent.Contains("Mac")) return "MacOS";
            if (userAgent.Contains("Linux")) return "Linux";
            return "Unknown";
        }
    }

}
