using AutoMapper;
using WebApplication1.DTOs;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointments;
        private readonly IUserRepository _users;
        private readonly IVehicleRepository _vehicles;
        private readonly IServiceRepository _services;
        private readonly IMapper _mapper;

        public AppointmentService(
            IAppointmentRepository appointments,
            IUserRepository users,
            IVehicleRepository vehicles,
            IServiceRepository services,
            IMapper mapper)
        {
            _appointments = appointments;
            _users = users;
            _vehicles = vehicles;
            _services = services;
            _mapper = mapper;
        }

        /// <summary>
        /// получить все записи
        /// </summary>
        public async Task<List<AppointmentDto>> GetAllAsync()
        {
            var list = await _appointments.GetAllAsync();
            return _mapper.Map<List<AppointmentDto>>(list);
        }

        /// <summary>
        /// получить запись по Id
        /// </summary>
        public async Task<AppointmentDto> GetByIdAsync(int id)
        {
            var entity = await _appointments.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException("Запись не найдена");
            return _mapper.Map<AppointmentDto>(entity);
        }

        /// <summary>
        /// создать запись
        /// </summary>
        public async Task<AppointmentDto> CreateAsync(CreateAppointmentDto dto)
        {
            if (dto.EndTime <= dto.StartTime)
                throw new ArgumentException("EndTime должен быть больше StartTime");

            var customer = await _users.GetByIdAsync(dto.CustomerId) ?? throw new KeyNotFoundException("Клиент не найден");
            var mechanic = await _users.GetByIdAsync(dto.MechanicId) ?? throw new KeyNotFoundException("Механик не найден");
            var vehicle = await _vehicles.GetByIdAsync(dto.VehicleId) ?? throw new KeyNotFoundException("Автомобиль не найден");
            var service = await _services.GetByIdAsync(dto.ServiceId) ?? throw new KeyNotFoundException("Услуга не найдена");

            if (vehicle.OwnerId != dto.CustomerId)
                throw new ArgumentException("Автомобиль не принадлежит данному клиенту");

            var entity = _mapper.Map<Appointment>(dto);
            var created = await _appointments.AddAsync(entity);

            var full = await _appointments.GetByIdAsync(created.Id);
            return _mapper.Map<AppointmentDto>(full!);
        }

        /// <summary>
        /// обновить запись
        /// </summary>
        public async Task<AppointmentDto> UpdateAsync(int id, UpdateAppointmentDto dto)
        {
            if (dto.EndTime <= dto.StartTime)
                throw new ArgumentException("EndTime должен быть больше StartTime");

            var existing = await _appointments.GetByIdAsync(id);
            if (existing == null) throw new KeyNotFoundException("Запись не найдена");

            var vehicle = await _vehicles.GetByIdAsync(dto.VehicleId) ?? throw new KeyNotFoundException("Автомобиль не найден");
            if (vehicle.OwnerId != dto.CustomerId)
                throw new ArgumentException("Автомобиль не принадлежит данному клиенту");

            if (await _users.GetByIdAsync(dto.CustomerId) == null) throw new KeyNotFoundException("Клиент не найден");
            if (await _users.GetByIdAsync(dto.MechanicId) == null) throw new KeyNotFoundException("Механик не найден");
            if (await _services.GetByIdAsync(dto.ServiceId) == null) throw new KeyNotFoundException("Услуга не найдена");

            existing.StartTime = dto.StartTime;
            existing.EndTime = dto.EndTime;
            existing.Status = dto.Status;
            existing.Comment = dto.Comment;
            existing.CustomerId = dto.CustomerId;
            existing.MechanicId = dto.MechanicId;
            existing.VehicleId = dto.VehicleId;
            existing.ServiceId = dto.ServiceId;

            await _appointments.UpdateAsync(existing);

            var full = await _appointments.GetByIdAsync(id);
            return _mapper.Map<AppointmentDto>(full!);
        }

        /// <summary>
        /// удалить запись
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var existing = await _appointments.GetByIdAsync(id);
            if (existing == null) throw new KeyNotFoundException("Запись не найдена");
            await _appointments.DeleteAsync(id);
        }
    }
}
