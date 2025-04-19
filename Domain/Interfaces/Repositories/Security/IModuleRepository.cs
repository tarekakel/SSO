using Domain.Security;
using Domain.Security.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories.Security
{

    public interface IModuleRepository
    {
        Task<IEnumerable<Module>> GetAllAsync();
        Task<Module?> GetByIdAsync(Guid id);
        Task<IEnumerable<Module>> GetBySystemIdAsync(Guid systemId);
        Task AddAsync(Module entity);
        Task UpdateAsync(Module entity);
        Task DeleteAsync(Guid id);
    }
}

