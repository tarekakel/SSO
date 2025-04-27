using Application.Validators;
using Domain.Interfaces.Repositories.Security;
using Domain.Interfaces.Services.Security;
using Domain.Security;
using Shared.General.Dtos;
using Shared.Security.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Services.Security
{
    public class SystemService : ISystemService
    {
        private readonly ISystemRepository _repo;
        private readonly CreateSystemDtoValidator _validator = new();

        public SystemService(ISystemRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<SystemDto>> GetAllAsync()
        {
            var systems = await _repo.GetAllAsync();
            return systems.Select(x => new SystemDto(x.Id, x.Name, x.Description));
        }

        public async Task<SystemDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : new SystemDto(entity.Id, entity.Name, entity.Description);
        }

        public async Task<GeneralResponse<string>> AddAsync(CreateSystemDto dto)
        {

            GeneralResponse<string> generalResponse = new GeneralResponse<string>();
            var validationResult = _validator.Validate(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return GeneralResponse<string>.ErrorResponse("Validation failed", errors);
            }


            var entity = new SystemEntity
            {
                Name = dto.Name,
                Description = dto.Description
            };
            await _repo.AddAsync(entity);


            return GeneralResponse<string>.SuccessResponse("Success");

        }

        public async Task UpdateAsync(UpdateSystemDto dto)
        {
            var entity = await _repo.GetByIdAsync(dto.Id);
            if (entity == null) return;

            entity.Name = dto.Name;
            entity.Description = dto.Description;

            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<GeneralResponse<PagedResult<SystemDto>>> Search(PagedRequest<SystemDto> pagedRequest)
        {
            PagedRequest<SystemEntity> pagedEntity = new PagedRequest<SystemEntity>()
            {

                PageSize = pagedRequest.PageSize,
                PageIndex = pagedRequest.PageIndex,
            };
            if (pagedRequest.Filter is not null)
            {
                pagedEntity.Filter = new SystemEntity()
                {
                    Name = pagedRequest.Filter.Name,
                    //    Id = pagedRequest.Filter.Id.Value,
                    Description = pagedRequest.Filter.Description
                };
            }


            var res = await _repo.Search(pagedEntity);
            PagedResult<SystemDto> result = new PagedResult<SystemDto>()
            {
                Data = res.Data.Select(w => new SystemDto(w.Id, w.Name, w.Description)).ToList(),
                PageIndex = res.PageIndex,
                PageSize = res.PageSize,
                TotalCount = res.TotalCount
            };

            return GeneralResponse<PagedResult<SystemDto>>.SuccessResponse(result);

        }
    }

}
