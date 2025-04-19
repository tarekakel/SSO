using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserSessionRepository : IUserSessionRepository
    {
        private readonly ApplicationDbContext _context;

        public UserSessionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserSession session)
        {
            await _context.UserSessions.AddAsync(session);
        }

        public async Task<UserSession?> GetLatestActiveSessionAsync(string userId)
        {
            return await _context.UserSessions
                .Where(x => x.UserId == userId && x.LogoutTime == null)
                .OrderByDescending(x => x.LoginTime)
                .FirstOrDefaultAsync();
        }

        public Task UpdateAsync(UserSession session)
        {
            _context.UserSessions.Update(session);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }


}
