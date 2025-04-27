using Asp.Versioning;
using Domain.Interfaces.Services.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.General.Dtos;
using Shared.Security.Dtos;
using SSO.Filters.Auth;

namespace SSO.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Roles = "Admin")]

    public class SystemController : ControllerBase
    {
        private readonly ISystemService _systemService;

        public SystemController(ISystemService systemService)
        {
            _systemService = systemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _systemService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var system = await _systemService.GetByIdAsync(id);
            return system == null ? NotFound() : Ok(system);
        }


        [HttpPost("Search")]
        public async Task<IActionResult> Search([FromBody] PagedRequest<SystemDto> request)
        {
            return Ok(await _systemService.Search(request));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSystemDto dto)
        {
            ;
            return Ok(await _systemService.AddAsync(dto));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateSystemDto dto)
        {
            await _systemService.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _systemService.DeleteAsync(id);
            return Ok();
        }
    }

}
