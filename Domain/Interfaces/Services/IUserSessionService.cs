using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IUserSessionService
    {
        Task LogUserSessionAsync(ApplicationUser user, HttpContext httpContext);
        Task MarkLogoutAsync(string userId);
    }

}
