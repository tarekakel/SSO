using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{

    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken token);
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task<IEnumerable<RefreshToken>> GetActiveTokensByUserIdAsync(string userId);
        Task InvalidateTokenAsync(string token);
        Task InvalidateAllTokensAsync(string userId);
        Task MarkAsUsedAsync(string token);
        Task SaveChangesAsync();
    }

}
