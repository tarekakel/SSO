using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{

    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RefreshTokenRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(RefreshToken token)
        {
            await _dbContext.RefreshTokens.AddAsync(token);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _dbContext.RefreshTokens
                .FirstOrDefaultAsync(r => r.Token == token && !r.IsRevoked && !r.IsUsed);
        }

        public async Task<IEnumerable<RefreshToken>> GetActiveTokensByUserIdAsync(string userId)
        {
            return await _dbContext.RefreshTokens
                .Where(r => r.UserId == userId && !r.IsRevoked)
                .ToListAsync();
        }

        public async Task InvalidateTokenAsync(string token)
        {
            var refreshToken = await _dbContext.RefreshTokens
                .FirstOrDefaultAsync(r => r.Token == token);

            if (refreshToken != null)
            {
                refreshToken.IsRevoked = true;
                _dbContext.RefreshTokens.Update(refreshToken);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task InvalidateAllTokensAsync(string userId)
        {
            var refreshTokens = await _dbContext.RefreshTokens
                .Where(r => r.UserId == userId && !r.IsRevoked)
                .ToListAsync();

            foreach (var token in refreshTokens)
            {
                token.IsRevoked = true;
                _dbContext.RefreshTokens.Update(token);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task MarkAsUsedAsync(string token)
        {
            var refreshToken = await _dbContext.RefreshTokens
                .FirstOrDefaultAsync(r => r.Token == token && !r.IsRevoked);

            if (refreshToken != null)
            {
                refreshToken.IsUsed = true;
                _dbContext.RefreshTokens.Update(refreshToken);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }

}
