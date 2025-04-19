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
    public class ModuleService : IModuleService
    {
        private readonly IModuleRepository _repo;

        public ModuleService(IModuleRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ModuleDto>> GetAllAsync()
        {
            var modules = await _repo.GetAllAsync();
            return modules.Select(x => new ModuleDto(x.Id, x.Name, x.SystemId));
        }

        public async Task<ModuleDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : new ModuleDto(entity.Id, entity.Name, entity.SystemId);
        }

        public async Task<IEnumerable<ModuleDto>> GetBySystemIdAsync(Guid systemId)
        {
            var modules = await _repo.GetBySystemIdAsync(systemId);
            return modules.Select(x => new ModuleDto(x.Id, x.Name, x.SystemId));
        }

        public async Task AddAsync(CreateModuleDto dto)
        {
            var entity = new Module
            {
                Name = dto.Name,
                SystemId = dto.SystemId,

            };

            await _repo.AddAsync(entity);
        }

        public async Task UpdateAsync(UpdateModuleDto dto)
        {
            var entity = await _repo.GetByIdAsync(dto.Id);
            if (entity == null) return;

            entity.Name = dto.Name;
            entity.SystemId = dto.SystemId;

            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }

}
