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
    public class SystemService : ISystemService
    {
        private readonly ISystemRepository _repo;

        public SystemService(ISystemRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<SystemDto>> GetAllAsync()
        {
            var systems = await _repo.GetAllAsync();
            return systems.Select(x => new SystemDto(x.Id, x.Name, x.Description));
        }

        public async Task<SystemDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : new SystemDto(entity.Id, entity.Name, entity.Description);
        }

        public async Task AddAsync(CreateSystemDto dto)
        {
            var entity = new SystemEntity
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await _repo.AddAsync(entity);
        }

        public async Task UpdateAsync(UpdateSystemDto dto)
        {
            var entity = await _repo.GetByIdAsync(dto.Id);
            if (entity == null) return;

            entity.Name = dto.Name;
            entity.Description = dto.Description;

            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }

}
