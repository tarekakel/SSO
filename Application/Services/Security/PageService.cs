using Domain.Interfaces.Repositories.Security;
using Domain.Interfaces.Services.Security;
using Domain.Security;
using Shared.Security.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Security
{
    public class PageService : IPageService
    {
        private readonly IPageRepository _repo;

        public PageService(IPageRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PageDto>> GetAllAsync()
        {
            var pages = await _repo.GetAllAsync();
            return pages.Select(x => new PageDto(x.Id, x.Name, x.ModuleId));
        }

        public async Task<PageDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : new PageDto(entity.Id, entity.Name, entity.ModuleId);
        }

        public async Task<IEnumerable<PageDto>> GetByModuleIdAsync(Guid moduleId)
        {
            var pages = await _repo.GetByModuleIdAsync(moduleId);
            return pages.Select(x => new PageDto(x.Id, x.Name, x.ModuleId));
        }

        public async Task AddAsync(CreatePageDto dto)
        {
            var entity = new Page
            {
                Name = dto.Name,
                ModuleId = dto.ModuleId
            };

            await _repo.AddAsync(entity);
        }

        public async Task UpdateAsync(UpdatePageDto dto)
        {
            var entity = await _repo.GetByIdAsync(dto.Id);
            if (entity == null) return;

            entity.Name = dto.Name;
            entity.ModuleId = dto.ModuleId;

            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }

}
