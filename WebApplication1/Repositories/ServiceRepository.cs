using Microsoft.EntityFrameworkCore;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly UsersDBContext _context;

        /// <summary>
        /// репозиторий услуг, работает через EF DbContext
        /// </summary>
        public ServiceRepository(UsersDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// получить список всех услуг
        /// </summary>
        public async Task<List<Service>> GetAllAsync()
            => await _context.Services.ToListAsync();

        /// <summary>
        /// получить услугу по Id
        /// </summary>
        public async Task<Service?> GetByIdAsync(int id)
            => await _context.Services.FindAsync(id);

        /// <summary>
        /// добавить новую услугу
        /// </summary>
        public async Task<Service> AddAsync(Service entity)
        {
            _context.Services.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// обновить услугу
        /// </summary>
        public async Task UpdateAsync(Service entity)
        {
            _context.Services.Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// удалить услугу по Id
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var item = await _context.Services.FindAsync(id);
            if (item == null) return;

            _context.Services.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
