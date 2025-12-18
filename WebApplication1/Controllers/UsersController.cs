using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Responses;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ApiResponse<List<UserDto>>> GetAll()
        {
            var data = await _service.GetAllAsync();
            return ApiResponse<List<UserDto>>.Ok(data, HttpContext.TraceIdentifier);
        }

        [HttpGet("{id:int}")]
        public async Task<ApiResponse<UserDto>> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            return ApiResponse<UserDto>.Ok(data, HttpContext.TraceIdentifier);
        }

        [HttpPost]
        public async Task<ApiResponse<UserDto>> Create([FromBody] CreateUserDto dto)
        {
            var data = await _service.CreateAsync(dto);
            return ApiResponse<UserDto>.Ok(data, HttpContext.TraceIdentifier);
        }

        [HttpPut("{id:int}")]
        public async Task<ApiResponse<UserDto>> Update(int id, [FromBody] UpdateUserDto dto)
        {
            var data = await _service.UpdateAsync(id, dto);
            return ApiResponse<UserDto>.Ok(data, HttpContext.TraceIdentifier);
        }

        [HttpDelete("{id:int}")]
        public async Task<ApiResponse<bool>> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return ApiResponse<bool>.Ok(true, HttpContext.TraceIdentifier);
        }
    }
}
