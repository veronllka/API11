using AutoMapper;
using WebApplication1.DTOs;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _services;
        private readonly IMapper _mapper;

        public ServiceService(IServiceRepository services, IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
        }

        /// <summary>
        /// получить все услуги
        /// </summary>
        public async Task<List<ServiceDto>> GetAllAsync()
        {
            var list = await _services.GetAllAsync();
            return _mapper.Map<List<ServiceDto>>(list);
        }

        /// <summary>
        /// получить услугу по Id
        /// </summary>
        public async Task<ServiceDto> GetByIdAsync(int id)
        {
            var entity = await _services.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException("Услуга не найдена");
            return _mapper.Map<ServiceDto>(entity);
        }

        /// <summary>
        /// создать услугу
        /// </summary>
        public async Task<ServiceDto> CreateAsync(CreateServiceDto dto)
        {
            if (dto.DurationMinutes <= 0) throw new ArgumentException("DurationMinutes должен быть > 0");
            if (dto.Price < 0) throw new ArgumentException("Price не может быть отрицательной");

            var entity = _mapper.Map<Service>(dto);
            var created = await _services.AddAsync(entity);
            return _mapper.Map<ServiceDto>(created);
        }

        /// <summary>
        /// обновить услугу
        /// </summary>
        public async Task<ServiceDto> UpdateAsync(int id, UpdateServiceDto dto)
        {
            var entity = await _services.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException("Услуга не найдена");

            if (dto.DurationMinutes <= 0) throw new ArgumentException("DurationMinutes должен быть > 0");
            if (dto.Price < 0) throw new ArgumentException("Price не может быть отрицательной");

            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.DurationMinutes = dto.DurationMinutes;
            entity.Price = dto.Price;

            await _services.UpdateAsync(entity);
            var updated = await _services.GetByIdAsync(id);
            return _mapper.Map<ServiceDto>(updated!);
        }

        /// <summary>
        /// удалить услугу
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var entity = await _services.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException("Услуга не найдена");
            await _services.DeleteAsync(id);
        }
    }
}
