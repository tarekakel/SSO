using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repository
{
    public interface IUserSessionRepository
    {
        Task AddAsync(UserSession session);
        Task<UserSession?> GetLatestActiveSessionAsync(string userId);
        Task UpdateAsync(UserSession session);
        Task SaveChangesAsync();
    }

}
