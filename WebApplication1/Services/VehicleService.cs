using AutoMapper;
using WebApplication1.DTOs;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicles;
        private readonly IUserRepository _users;
        private readonly IMapper _mapper;

        public VehicleService(IVehicleRepository vehicles, IUserRepository users, IMapper mapper)
        {
            _vehicles = vehicles;
            _users = users;
            _mapper = mapper;
        }

        /// <summary>
        /// получить все автомобили
        /// </summary>
        public async Task<List<VehicleDto>> GetAllAsync()
        {
            var list = await _vehicles.GetAllAsync();
            return _mapper.Map<List<VehicleDto>>(list);
        }

        /// <summary>
        /// получить автомобиль по Id
        /// </summary>
        public async Task<VehicleDto> GetByIdAsync(int id)
        {
            var entity = await _vehicles.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException("Автомобиль не найден");
            return _mapper.Map<VehicleDto>(entity);
        }

        /// <summary>
        /// создать автомобиль
        /// </summary>
        public async Task<VehicleDto> CreateAsync(CreateVehicleDto dto)
        {
            var owner = await _users.GetByIdAsync(dto.OwnerId);
            if (owner == null) throw new KeyNotFoundException("Владелец (User) не найден");

            var entity = _mapper.Map<Vehicle>(dto);
            var created = await _vehicles.AddAsync(entity);

            var full = await _vehicles.GetByIdAsync(created.Id);
            return _mapper.Map<VehicleDto>(full!);
        }

        /// <summary>
        /// обновить автомобиль
        /// </summary>
        public async Task<VehicleDto> UpdateAsync(int id, UpdateVehicleDto dto)
        {
            var entity = await _vehicles.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException("Автомобиль не найден");

            var owner = await _users.GetByIdAsync(dto.OwnerId);
            if (owner == null) throw new KeyNotFoundException("Владелец (User) не найден");

            entity.Brand = dto.Brand;
            entity.Model = dto.Model;
            entity.PlateNumber = dto.PlateNumber;
            entity.Year = dto.Year;
            entity.OwnerId = dto.OwnerId;

            await _vehicles.UpdateAsync(entity);

            var full = await _vehicles.GetByIdAsync(id);
            return _mapper.Map<VehicleDto>(full!);
        }

        /// <summary>
        /// удалить автомобиль
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var entity = await _vehicles.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException("Автомобиль не найден");
            await _vehicles.DeleteAsync(id);
        }
    }
}
