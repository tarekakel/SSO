using Shared.General.Dtos;
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
        Task<GeneralResponse<PagedResult<SystemDto>>> Search(PagedRequest<SystemDto> pagedRequest);

        Task<SystemDto?> GetByIdAsync(Guid id);
        Task<GeneralResponse<string>> AddAsync(CreateSystemDto dto);
        Task UpdateAsync(UpdateSystemDto dto);
        Task DeleteAsync(Guid id);
    }
}
