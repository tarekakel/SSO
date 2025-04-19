using Domain.Security;
using Domain.Security.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories.Security
{
    public interface ISystemRepository
    {
        Task<IEnumerable<SystemEntity>> GetAllAsync();
        Task<SystemEntity?> GetByIdAsync(Guid id);
        Task AddAsync(SystemEntity entity);
        Task UpdateAsync(SystemEntity entity);
        Task DeleteAsync(Guid id);
    }






}
