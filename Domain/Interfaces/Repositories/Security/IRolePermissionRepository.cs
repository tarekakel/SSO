using Domain.Security;
using Domain.Security.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories.Security
{
    public interface IRolePermissionRepository
    {
        Task<IEnumerable<Permission>> GetPermissionsByRoleIdAsync(string roleId);
        Task AddAsync(string roleId, Guid permissionId);
        Task RemoveAsync(string roleId, Guid permissionId);
        Task<bool> RoleHasPermissionAsync(string roleId, string permissionCode);
    }
}

