using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Responses;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _service;

        public RolesController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ApiResponse<List<RoleDto>>> GetAll()
        {
            var data = await _service.GetAllAsync();
            return ApiResponse<List<RoleDto>>.Ok(data, HttpContext.TraceIdentifier);
        }

        [HttpGet("{id:int}")]
        public async Task<ApiResponse<RoleDto>> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            return ApiResponse<RoleDto>.Ok(data, HttpContext.TraceIdentifier);
        }

        [HttpPost]
        public async Task<ApiResponse<RoleDto>> Create([FromBody] CreateRoleDto dto)
        {
            var data = await _service.CreateAsync(dto);
            return ApiResponse<RoleDto>.Ok(data, HttpContext.TraceIdentifier);
        }

        [HttpPut("{id:int}")]
        public async Task<ApiResponse<RoleDto>> Update(int id, [FromBody] UpdateRoleDto dto)
        {
            var data = await _service.UpdateAsync(id, dto);
            return ApiResponse<RoleDto>.Ok(data, HttpContext.TraceIdentifier);
        }

        [HttpDelete("{id:int}")]
        public async Task<ApiResponse<bool>> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return ApiResponse<bool>.Ok(true, HttpContext.TraceIdentifier);
        }
    }
}
