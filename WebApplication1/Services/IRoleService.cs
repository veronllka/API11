using WebApplication1.DTOs;

namespace WebApplication1.Services
{
    public interface IRoleService
    {
        /// <summary>
        /// получить список всех ролей
        /// </summary>
        Task<List<RoleDto>> GetAllAsync();

        /// <summary>
        /// получить роль по идентификатору
        /// </summary>
        Task<RoleDto> GetByIdAsync(int id);

        /// <summary>
        /// создать новую роль
        /// </summary>
        Task<RoleDto> CreateAsync(CreateRoleDto dto);

        /// <summary>
        /// обновить роль по идентификатору
        /// </summary>
        Task<RoleDto> UpdateAsync(int id, UpdateRoleDto dto);

        /// <summary>
        /// удалить роль по идентификатору
        /// </summary>
        Task DeleteAsync(int id);
    }
}
