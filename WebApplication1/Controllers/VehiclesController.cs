using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Responses;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _service;

        public VehiclesController(IVehicleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ApiResponse<List<VehicleDto>>> GetAll()
        {
            var data = await _service.GetAllAsync();
            return ApiResponse<List<VehicleDto>>.Ok(data, HttpContext.TraceIdentifier);
        }

        [HttpGet("{id:int}")]
        public async Task<ApiResponse<VehicleDto>> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            return ApiResponse<VehicleDto>.Ok(data, HttpContext.TraceIdentifier);
        }

        [HttpPost]
        public async Task<ApiResponse<VehicleDto>> Create([FromBody] CreateVehicleDto dto)
        {
            var data = await _service.CreateAsync(dto);
            return ApiResponse<VehicleDto>.Ok(data, HttpContext.TraceIdentifier);
        }

        [HttpPut("{id:int}")]
        public async Task<ApiResponse<VehicleDto>> Update(int id, [FromBody] UpdateVehicleDto dto)
        {
            var data = await _service.UpdateAsync(id, dto);
            return ApiResponse<VehicleDto>.Ok(data, HttpContext.TraceIdentifier);
        }

        [HttpDelete("{id:int}")]
        public async Task<ApiResponse<bool>> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return ApiResponse<bool>.Ok(true, HttpContext.TraceIdentifier);
        }
    }
}
