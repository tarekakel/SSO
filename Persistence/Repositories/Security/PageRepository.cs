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
    public class PageRepository : IPageRepository
    {
        private readonly ApplicationDbContext _context;

        public PageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Page>> GetAllAsync() =>
            await _context.Pages.ToListAsync();

        public async Task<Page?> GetByIdAsync(Guid id) =>
            await _context.Pages.FirstOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(Page entity)
        {
            await _context.Pages.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Page entity)
        {
            _context.Pages.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return;
            entity.IsDeleted = true;
            await UpdateAsync(entity);
        }

        public Task<IEnumerable<Page>> GetByModuleIdAsync(Guid moduleId)
        {
            throw new NotImplementedException();
        }
    }

}
