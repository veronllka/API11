using Microsoft.EntityFrameworkCore;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly UsersDBContext _context;

        public ServiceRepository(UsersDBContext context)
        {
            _context = context;
        }

        public async Task<List<Service>> GetAllAsync()
            => await _context.Services.ToListAsync();

        public async Task<Service?> GetByIdAsync(int id)
            => await _context.Services.FindAsync(id);

        public async Task<Service> AddAsync(Service entity)
        {
            _context.Services.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Service entity)
        {
            _context.Services.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.Services.FindAsync(id);
            if (item == null) return;

            _context.Services.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
