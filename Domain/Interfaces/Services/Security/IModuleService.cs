using Domain.Security;
using Shared.Security.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Domain.Interfaces.Services.Security
{
    public interface IModuleService
    {
        Task<IEnumerable<ModuleDto>> GetAllAsync();
        Task<ModuleDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<ModuleDto>> GetBySystemIdAsync(Guid systemId);
        Task AddAsync(CreateModuleDto dto);
        Task UpdateAsync(UpdateModuleDto dto);
        Task DeleteAsync(Guid id);
    }
}
