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
    public class PageController : ControllerBase
    {
        private readonly IPageService _pageService;

        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _pageService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) =>
            Ok(await _pageService.GetByIdAsync(id));

        [HttpGet("by-module/{moduleId}")]
        public async Task<IActionResult> GetByModuleId(Guid moduleId) =>
            Ok(await _pageService.GetByModuleIdAsync(moduleId));

        [HttpPost]
        public async Task<IActionResult> Create(CreatePageDto dto)
        {
            await _pageService.AddAsync(dto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdatePageDto dto)
        {
            await _pageService.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _pageService.DeleteAsync(id);
            return Ok();
        }
    }

}
