using Domain.Interfaces.Repositories.Security;
using Domain.Security;
using Domain.Security.Enums;
using Infrastructure.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Security
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDbContext _context;

        public PermissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Permission>> GetAllAsync() =>
            await _context.Permissions.Where(x => !x.IsDeleted).ToListAsync();
        public async Task<Permission?> GetByIdAsync(Guid id) =>
            await _context.Permissions.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        public async Task AddAsync(Permission entity)
        {
            await _context.Permissions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Permission entity)
        {
            _context.Permissions.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return;
            entity.IsDeleted = true;
            await UpdateAsync(entity);
        }

        public Task<Permission?> GetByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Permission>> GetByPageIdAsync(Guid pageId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Permission>> GetByTypeAsync(PermissionType type)
        {
            throw new NotImplementedException();
        }
    }
}
