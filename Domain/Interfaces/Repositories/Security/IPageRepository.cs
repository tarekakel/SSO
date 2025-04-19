using Domain.Security;
using Domain.Security.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories.Security
{

    public interface IPageRepository
    {
        Task<IEnumerable<Page>> GetAllAsync();
        Task<Page?> GetByIdAsync(Guid id);
        Task<IEnumerable<Page>> GetByModuleIdAsync(Guid moduleId);
        Task AddAsync(Page entity);
        Task UpdateAsync(Page entity);
        Task DeleteAsync(Guid id);
    }
}

