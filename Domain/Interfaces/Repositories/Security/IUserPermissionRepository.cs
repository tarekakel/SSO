using Domain.Security;
using Domain.Security.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories.Security
{
    public interface IUserPermissionRepository // Optional for user-level overrides
    {
        Task<IEnumerable<Permission>> GetPermissionsByUserIdAsync(string userId);
        Task AddAsync(string userId, Guid permissionId);
        Task RemoveAsync(string userId, Guid permissionId);
        Task<bool> UserHasPermissionAsync(string userId, string permissionCode);
    }
}

