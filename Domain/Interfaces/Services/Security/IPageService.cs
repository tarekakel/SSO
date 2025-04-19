using Shared.Security.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services.Security
{
    public interface IPageService
    {
        Task<IEnumerable<PageDto>> GetAllAsync();
        Task<PageDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<PageDto>> GetByModuleIdAsync(Guid moduleId);
        Task AddAsync(CreatePageDto dto);
        Task UpdateAsync(UpdatePageDto dto);
        Task DeleteAsync(Guid id);
    }
}
