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
    public class ModuleController : ControllerBase
    {
        private readonly IModuleService _moduleService;

        public ModuleController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _moduleService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) =>
            Ok(await _moduleService.GetByIdAsync(id));

        [HttpGet("by-system/{systemId}")]
        public async Task<IActionResult> GetBySystemId(Guid systemId) =>
            Ok(await _moduleService.GetBySystemIdAsync(systemId));

        [HttpPost]
        public async Task<IActionResult> Create(CreateModuleDto dto)
        {
            await _moduleService.AddAsync(dto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateModuleDto dto)
        {
            await _moduleService.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _moduleService.DeleteAsync(id);
            return Ok();
        }
    }
    /// <summary>
    /// /////////////////
    /// </summary>






}
