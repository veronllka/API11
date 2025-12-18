using WebApplication1.DTOs;

namespace WebApplication1.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(int id);
        Task<UserDto> CreateAsync(CreateUserDto dto);

        Task<UserDto> UpdateAsync(int id, UpdateUserDto dto);

        Task DeleteAsync(int id);
    }
}
