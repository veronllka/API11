using AutoMapper;
using WebApplication1.DTOs;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roles;
        private readonly IMapper _mapper;

        /// <summary>
        /// сервис бизнес-логики для ролей
        /// </summary>
        public RoleService(IRoleRepository roles, IMapper mapper)
        {
            _roles = roles;
            _mapper = mapper;
        }

        /// <summary>
        /// получить список всех ролей
        /// </summary>
        public async Task<List<RoleDto>> GetAllAsync()
        {
            var list = await _roles.GetAllAsync();
            return _mapper.Map<List<RoleDto>>(list);
        }

        /// <summary>
        /// получить роль по Id
        /// </summary>
        public async Task<RoleDto> GetByIdAsync(int id)
        {
            var role = await _roles.GetByIdAsync(id);
            if (role == null) throw new KeyNotFoundException("Роль не найдена");
            return _mapper.Map<RoleDto>(role);
        }

        /// <summary>
        /// создать роль
        /// </summary>
        public async Task<RoleDto> CreateAsync(CreateRoleDto dto)
        {
            var entity = _mapper.Map<Role>(dto);
            var created = await _roles.AddAsync(entity);
            return _mapper.Map<RoleDto>(created);
        }

        /// <summary>
        /// обновить роль
        /// </summary>
        public async Task<RoleDto> UpdateAsync(int id, UpdateRoleDto dto)
        {
            var role = await _roles.GetByIdAsync(id);
            if (role == null) throw new KeyNotFoundException("Роль не найдена");

            role.Name = dto.Name;
            role.Description = dto.Description;

            await _roles.UpdateAsync(role);

            var updated = await _roles.GetByIdAsync(id);
            return _mapper.Map<RoleDto>(updated!);
        }

        /// <summary>
        /// Уудалить роль
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var role = await _roles.GetByIdAsync(id);
            if (role == null) throw new KeyNotFoundException("Роль не найдена");
            await _roles.DeleteAsync(id);
        }
    }
}
