using Domain.Security;
using Domain.Security.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories.Security
{

    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetAllAsync();
        Task<Permission?> GetByIdAsync(Guid id);
        Task<Permission?> GetByCodeAsync(string code);
        Task<IEnumerable<Permission>> GetByPageIdAsync(Guid pageId);
        Task<IEnumerable<Permission>> GetByTypeAsync(PermissionType type);
        Task AddAsync(Permission entity);
        Task UpdateAsync(Permission entity);
        Task DeleteAsync(Guid id);
    }

}

