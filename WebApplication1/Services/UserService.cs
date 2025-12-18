using AutoMapper;
using WebApplication1.DTOs;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _users;
        private readonly IRoleRepository _roles;
        private readonly IMapper _mapper;

        public UserService(IUserRepository users, IRoleRepository roles, IMapper mapper)
        {
            _users = users;
            _roles = roles;
            _mapper = mapper;
        }

        /// <summary>
        /// получить всех пользователей
        /// </summary>
        public async Task<List<UserDto>> GetAllAsync()
        {
            var list = await _users.GetAllAsync();
            return _mapper.Map<List<UserDto>>(list);
        }

        /// <summary>
        /// получить пользователя по Id
        /// </summary>
        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _users.GetByIdAsync(id);
            if (user == null) throw new KeyNotFoundException("Пользователь не найден");
            return _mapper.Map<UserDto>(user);
        }

        /// <summary>
        /// создать пользователя
        /// </summary>
        public async Task<UserDto> CreateAsync(CreateUserDto dto)
        {
            var role = await _roles.GetByIdAsync(dto.RoleId);
            if (role == null) throw new KeyNotFoundException("Роль не найдена");

            var existingByEmail = await _users.GetByEmailAsync(dto.Email);
            if (existingByEmail != null) throw new ArgumentException("Email уже используется");

            var entity = _mapper.Map<User>(dto);
            var created = await _users.AddAsync(entity);

            var full = await _users.GetByIdAsync(created.Id);
            return _mapper.Map<UserDto>(full!);
        }

        /// <summary>
        /// обновить пользователя
        /// </summary>
        public async Task<UserDto> UpdateAsync(int id, UpdateUserDto dto)
        {
            var user = await _users.GetByIdAsync(id);
            if (user == null) throw new KeyNotFoundException("Пользователь не найден");

            var role = await _roles.GetByIdAsync(dto.RoleId);
            if (role == null) throw new KeyNotFoundException("Роль не найдена");

            if (!string.Equals(user.Email, dto.Email, StringComparison.OrdinalIgnoreCase))
            {
                var existingByEmail = await _users.GetByEmailAsync(dto.Email);
                if (existingByEmail != null) throw new ArgumentException("Email уже используется");
            }

            user.Name = dto.Name;
            user.Email = dto.Email;
            user.Phone = dto.Phone;
            user.RoleId = dto.RoleId;

            await _users.UpdateAsync(user);

            var full = await _users.GetByIdAsync(id);
            return _mapper.Map<UserDto>(full!);
        }


        /// <summary>
        /// удалить пользователя
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var user = await _users.GetByIdAsync(id);
            if (user == null) throw new KeyNotFoundException("Пользователь не найден");
            await _users.DeleteAsync(id);
        }
    }
}
