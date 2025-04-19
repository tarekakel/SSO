using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Auth
{
    public interface ITokenService
    {
        public string GenerateToken(ApplicationUser user, IList<string> roles);
        public Task<(string AccessToken, string RefreshToken)> GenerateTokensWithRefreshAsync(ApplicationUser user, IList<string> roles);
    }
}
