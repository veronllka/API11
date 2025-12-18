using WebApplication1.DTOs;

namespace WebApplication1.Services
{
    public interface IVehicleService
    {
        /// <summary>
        /// получить все автомобили
        /// </summary>
        Task<List<VehicleDto>> GetAllAsync();

        /// <summary>
        /// получить автомобиль по Id
        /// </summary>
        Task<VehicleDto> GetByIdAsync(int id);

        /// <summary>
        /// создать автомобиль
        /// </summary>
        Task<VehicleDto> CreateAsync(CreateVehicleDto dto);

        /// <summary>
        /// обновить автомобиль
        /// </summary>
        Task<VehicleDto> UpdateAsync(int id, UpdateVehicleDto dto);

        /// <summary>
        /// удалить автомобиль
        /// </summary>
        Task DeleteAsync(int id);
    }
}
