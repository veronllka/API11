using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Responses;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _service;

        public ServicesController(IServiceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ApiResponse<List<ServiceDto>>> GetAll()
        {
            var data = await _service.GetAllAsync();
            return ApiResponse<List<ServiceDto>>.Ok(data, HttpContext.TraceIdentifier);
        }

        [HttpGet("{id:int}")]
        public async Task<ApiResponse<ServiceDto>> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            return ApiResponse<ServiceDto>.Ok(data, HttpContext.TraceIdentifier);
        }

        [HttpPost]
        public async Task<ApiResponse<ServiceDto>> Create([FromBody] CreateServiceDto dto)
        {
            var data = await _service.CreateAsync(dto);
            return ApiResponse<ServiceDto>.Ok(data, HttpContext.TraceIdentifier);
        }

        [HttpPut("{id:int}")]
        public async Task<ApiResponse<ServiceDto>> Update(int id, [FromBody] UpdateServiceDto dto)
        {
            var data = await _service.UpdateAsync(id, dto);
            return ApiResponse<ServiceDto>.Ok(data, HttpContext.TraceIdentifier);
        }

        [HttpDelete("{id:int}")]
        public async Task<ApiResponse<bool>> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return ApiResponse<bool>.Ok(true, HttpContext.TraceIdentifier);
        }
    }
}
