using Shared.Security.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services.Security
{
    public interface IPermissionService
    {
        Task<IEnumerable<PermissionDto>> GetAllAsync();
        Task<PermissionDto?> GetByIdAsync(Guid id);
        Task<PermissionDto?> GetByCodeAsync(string code);
        Task<IEnumerable<PermissionDto>> GetByPageIdAsync(Guid pageId);
        Task AddAsync(CreatePermissionDto dto);
        Task UpdateAsync(UpdatePermissionDto dto);
        Task DeleteAsync(Guid id);
    }
}
