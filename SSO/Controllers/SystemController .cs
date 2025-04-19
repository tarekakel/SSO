using Asp.Versioning;
using Domain.Interfaces.Services.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Security.Dtos;
using SSO.Filters.Auth;

namespace SSO.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSystemDto dto)
        {
            await _systemService.AddAsync(dto);
            return Ok();
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
