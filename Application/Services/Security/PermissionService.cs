using Domain.Interfaces.Repositories.Security;
using Domain.Interfaces.Services.Security;
using Domain.Security;
using Shared.Security.Dtos;


namespace Application.Services.Security
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _repo;

        public PermissionService(IPermissionRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PermissionDto>> GetAllAsync()
        {
            var perms = await _repo.GetAllAsync();
            return perms.Select(x => new PermissionDto(x.Id, x.Code, x.Description, x.PageId.Value));
        }

        public async Task<PermissionDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : new PermissionDto(entity.Id, entity.Code, entity.Description, entity.PageId.Value);
        }

        public async Task<PermissionDto?> GetByCodeAsync(string code)
        {
            var entity = await _repo.GetByCodeAsync(code);
            return entity == null ? null : new PermissionDto(entity.Id, entity.Code, entity.Description, entity.PageId.Value);
        }

        public async Task<IEnumerable<PermissionDto>> GetByPageIdAsync(Guid pageId)
        {
            var perms = await _repo.GetByPageIdAsync(pageId);
            return perms.Select(x => new PermissionDto(x.Id, x.Code, x.Description, x.PageId.Value));
        }

        public async Task AddAsync(CreatePermissionDto dto)
        {
            var entity = new Permission
            {
                Name = dto.Code,
                Code = dto.Code,
                Description = dto.Description,
                PageId = dto.PageId
            };

            await _repo.AddAsync(entity);
        }

        public async Task UpdateAsync(UpdatePermissionDto dto)
        {
            var entity = await _repo.GetByIdAsync(dto.Id);
            if (entity == null) return;

            entity.Code = dto.Code;
            entity.Description = dto.Description;
            entity.PageId = dto.PageId;

            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }

}
