using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Responses;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _service;

        public AppointmentsController(IAppointmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ApiResponse<List<AppointmentDto>>> GetAll()
        {
            var data = await _service.GetAllAsync();
            return ApiResponse<List<AppointmentDto>>.Ok(data, HttpContext.TraceIdentifier);
        }

        [HttpGet("{id:int}")]
        public async Task<ApiResponse<AppointmentDto>> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            return ApiResponse<AppointmentDto>.Ok(data, HttpContext.TraceIdentifier);
        }

        [HttpPost]
        public async Task<ApiResponse<AppointmentDto>> Create([FromBody] CreateAppointmentDto dto)
        {
            var data = await _service.CreateAsync(dto);
            return ApiResponse<AppointmentDto>.Ok(data, HttpContext.TraceIdentifier);
        }

        [HttpPut("{id:int}")]
        public async Task<ApiResponse<AppointmentDto>> Update(int id, [FromBody] UpdateAppointmentDto dto)
        {
            var data = await _service.UpdateAsync(id, dto);
            return ApiResponse<AppointmentDto>.Ok(data, HttpContext.TraceIdentifier);
        }

        [HttpDelete("{id:int}")]
        public async Task<ApiResponse<bool>> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return ApiResponse<bool>.Ok(true, HttpContext.TraceIdentifier);
        }
    }
}
