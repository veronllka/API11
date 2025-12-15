using Microsoft.EntityFrameworkCore;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly UsersDBContext _context;

        public VehicleRepository(UsersDBContext context)
        {
            _context = context;
        }

        public async Task<List<Vehicle>> GetAllAsync()
            => await _context.Vehicles.Include(v => v.Owner).ToListAsync();

        public async Task<Vehicle?> GetByIdAsync(int id)
            => await _context.Vehicles.Include(v => v.Owner)
                                      .FirstOrDefaultAsync(v => v.Id == id);

        public async Task<Vehicle> AddAsync(Vehicle entity)
        {
            _context.Vehicles.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Vehicle entity)
        {
            _context.Vehicles.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.Vehicles.FindAsync(id);
            if (item == null) return;

            _context.Vehicles.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
