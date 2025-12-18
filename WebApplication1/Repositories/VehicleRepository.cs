using Microsoft.EntityFrameworkCore;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    /// <summary>
    /// репозиторий автомобилей
    /// </summary>
    public class VehicleRepository : IVehicleRepository
    {
        private readonly UsersDBContext _context;

        /// <summary>
        /// инициализирует репозиторий автомобилей
        /// </summary>
        public VehicleRepository(UsersDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// получить список всех автомобилей
        /// </summary>
        public async Task<List<Vehicle>> GetAllAsync()
            => await _context.Vehicles.Include(v => v.Owner).ToListAsync();

        /// <summary>
        /// получить автомобиль по Id
        /// </summary>
        public async Task<Vehicle?> GetByIdAsync(int id)
            => await _context.Vehicles
                .Include(v => v.Owner)
                .FirstOrDefaultAsync(v => v.Id == id);

        /// <summary>
        /// добавить новый автомобиль
        /// </summary>
        public async Task<Vehicle> AddAsync(Vehicle entity)
        {
            _context.Vehicles.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// обновить автомобиль
        /// </summary>
        public async Task UpdateAsync(Vehicle entity)
        {
            _context.Vehicles.Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        ///удалить автомобиль по Id
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var item = await _context.Vehicles.FindAsync(id);
            if (item == null) return;

            _context.Vehicles.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
