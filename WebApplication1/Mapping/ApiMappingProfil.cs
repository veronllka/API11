using AutoMapper;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Responses;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication1.Mapping
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            // Role
            CreateMap<Role, RoleDto>();
            CreateMap<CreateRoleDto, Role>();
            CreateMap<UpdateRoleDto, Role>();

            // User
            CreateMap<User, UserDto>()
                .ForMember(d => d.RoleName, o => o.MapFrom(s => s.Role.Name));
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();

            // Service
            CreateMap<Service, ServiceDto>();
            CreateMap<CreateServiceDto, Service>();
            CreateMap<UpdateServiceDto, Service>();

            // Vehicle
            CreateMap<Vehicle, VehicleDto>()
                .ForMember(d => d.OwnerName, o => o.MapFrom(s => s.Owner.Name));
            CreateMap<CreateVehicleDto, Vehicle>();
            CreateMap<UpdateVehicleDto, Vehicle>();

            // Appointment
            CreateMap<Appointment, AppointmentDto>()
                .ForMember(d => d.CustomerName, o => o.MapFrom(s => s.Customer.Name))
                .ForMember(d => d.MechanicName, o => o.MapFrom(s => s.Mechanic.Name))
                .ForMember(d => d.ServiceName, o => o.MapFrom(s => s.Service.Name))
                .ForMember(d => d.VehicleInfo, o => o.MapFrom(s => $"{s.Vehicle.Brand} {s.Vehicle.Model} {s.Vehicle.PlateNumber}"));
            CreateMap<CreateAppointmentDto, Appointment>();
            CreateMap<UpdateAppointmentDto, Appointment>();
        }
    }
}
