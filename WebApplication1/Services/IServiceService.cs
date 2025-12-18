using WebApplication1.DTOs;

namespace WebApplication1.Services
{
    public interface IServiceService
    {
        /// <summary>
        /// получить все услуги
        /// </summary>
        Task<List<ServiceDto>> GetAllAsync();

        /// <summary>
        /// получить услугу по Id
        /// </summary>
        Task<ServiceDto> GetByIdAsync(int id);

        /// <summary>
        /// создать услугу
        /// </summary>
        Task<ServiceDto> CreateAsync(CreateServiceDto dto);

        /// <summary>
        /// обновить услугу
        /// </summary>
        Task<ServiceDto> UpdateAsync(int id, UpdateServiceDto dto);

        /// <summary>
        /// удалить услугу
        /// </summary>
        Task DeleteAsync(int id);
    }
}
