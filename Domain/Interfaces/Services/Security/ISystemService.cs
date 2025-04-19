using Shared.Security.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services.Security
{
    public interface ISystemService
    {
        Task<IEnumerable<SystemDto>> GetAllAsync();
        Task<SystemDto?> GetByIdAsync(Guid id);
        Task AddAsync(CreateSystemDto dto);
        Task UpdateAsync(UpdateSystemDto dto);
        Task DeleteAsync(Guid id);
    }
}
