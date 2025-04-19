using Domain.Interfaces.Repositories.Security;
using Domain.Security;
using Infrastructure.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Security
{
    public class SystemRepository : ISystemRepository
    {
        private readonly ApplicationDbContext _context;

        public SystemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SystemEntity>> GetAllAsync() =>
            await _context.SystemEntities.ToListAsync();

        public async Task<SystemEntity?> GetByIdAsync(Guid id) =>
            await _context.SystemEntities.FirstOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(SystemEntity entity)
        {
            await _context.SystemEntities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SystemEntity entity)
        {
            _context.SystemEntities.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return;
            entity.IsDeleted = true;
            await UpdateAsync(entity);
        }
    }
}
