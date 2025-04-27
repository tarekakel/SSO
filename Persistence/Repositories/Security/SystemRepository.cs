using Domain.Interfaces.Repositories.Security;
using Domain.Security;
using Infrastructure.Db;
using Microsoft.EntityFrameworkCore;
using Shared.General.Builder;
using Shared.General.Dtos;
using Shared.Security.Dtos;
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

        public async Task<PagedResult<SystemEntity>> Search(PagedRequest<SystemEntity> pagedRequest)
        {

            var entityFilterExpression = pagedRequest.Filter is not null
                     ? ExpressionBuilder.BuildFilterExpression(pagedRequest.Filter)
                     : x => true; // apply no filtering if filter is null
            var query = _context.SystemEntities.AsQueryable();

            query = query.Where(entityFilterExpression);

            var totalCount = await query.CountAsync();

            var data = await query
                .Skip((pagedRequest.PageIndex - 1) * pagedRequest.PageSize)
                .Take(pagedRequest.PageSize)
                .ToListAsync();



            return new PagedResult<SystemEntity>
            {
                TotalCount = totalCount,
                PageIndex = pagedRequest.PageIndex,
                PageSize = pagedRequest.PageSize,
                Data = data
            };
        }
    }
}
