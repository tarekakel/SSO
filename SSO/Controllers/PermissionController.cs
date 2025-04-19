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
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _permissionService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) =>
            Ok(await _permissionService.GetByIdAsync(id));

        [HttpGet("by-code/{code}")]
        public async Task<IActionResult> GetByCode(string code) =>
            Ok(await _permissionService.GetByCodeAsync(code));

        [HttpGet("by-page/{pageId}")]
        public async Task<IActionResult> GetByPageId(Guid pageId) =>
            Ok(await _permissionService.GetByPageIdAsync(pageId));

        [HttpPost]
        public async Task<IActionResult> Create(CreatePermissionDto dto)
        {
            await _permissionService.AddAsync(dto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdatePermissionDto dto)
        {
            await _permissionService.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _permissionService.DeleteAsync(id);
            return Ok();
        }
    }

}
