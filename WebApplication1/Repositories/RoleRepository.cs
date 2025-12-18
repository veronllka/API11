using Microsoft.EntityFrameworkCore;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    /// <summary>
    /// репозиторий ролей
    /// </summary>
    public class RoleRepository : IRoleRepository
    {
        private readonly UsersDBContext _context;

        /// <summary>
        /// инициализирует репозиторий ролей
        /// </summary>
        public RoleRepository(UsersDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// получить список всех ролей
        /// </summary>
        public async Task<List<Role>> GetAllAsync()
            => await _context.Roles.ToListAsync();

        /// <summary>
        /// получить роль по Id
        /// </summary>
        public async Task<Role?> GetByIdAsync(int id)
            => await _context.Roles.FindAsync(id);

        /// <summary>
        /// добавить новую роль
        /// </summary>
        public async Task<Role> AddAsync(Role entity)
        {
            _context.Roles.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// обновить роль
        /// </summary>
        public async Task UpdateAsync(Role entity)
        {
            _context.Roles.Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// удалить роль по Id
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null) return;

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }
    }
}
