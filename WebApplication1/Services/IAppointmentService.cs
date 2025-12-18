using WebApplication1.DTOs;

namespace WebApplication1.Services
{
    public interface IAppointmentService
    {
        /// <summary>
        /// получить все записи
        /// </summary>
        Task<List<AppointmentDto>> GetAllAsync();

        /// <summary>
        /// получить запись по Id
        /// </summary>
        Task<AppointmentDto> GetByIdAsync(int id);

        /// <summary>
        /// создать запись
        /// </summary>
        Task<AppointmentDto> CreateAsync(CreateAppointmentDto dto);

        /// <summary>
        /// обновить запись
        /// </summary>
        Task<AppointmentDto> UpdateAsync(int id, UpdateAppointmentDto dto);

        /// <summary>
        /// удалить запись
        /// </summary>
        Task DeleteAsync(int id);
    }
}
