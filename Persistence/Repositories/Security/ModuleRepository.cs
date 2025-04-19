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
    public class ModuleRepository : IModuleRepository
    {
        private readonly ApplicationDbContext _context;

        public ModuleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Module>> GetAllAsync() =>
            await _context.Modules.ToListAsync();

        public async Task<Module?> GetByIdAsync(Guid id) =>
            await _context.Modules.FirstOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(Module entity)
        {
            await _context.Modules.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Module entity)
        {
            _context.Modules.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return;
            entity.IsDeleted = true;
            await UpdateAsync(entity);
        }

        public Task<IEnumerable<Module>> GetBySystemIdAsync(Guid systemId)
        {
            throw new NotImplementedException();
        }
    }
}
