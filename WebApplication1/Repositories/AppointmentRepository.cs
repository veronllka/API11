using Microsoft.EntityFrameworkCore;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    /// <summary>
    /// репозиторий записей на обслуживание
    /// </summary>
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly UsersDBContext _context;

        /// <summary>
        /// инициализирует репозиторий записей
        /// </summary>
        public AppointmentRepository(UsersDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// получить список всех записей
        /// </summary>
        public async Task<List<Appointment>> GetAllAsync()
            => await _context.Appointments
                .Include(a => a.Customer)
                .Include(a => a.Mechanic)
                .Include(a => a.Vehicle)
                .Include(a => a.Service)
                .ToListAsync();

        /// <summary>
        /// получить запись по Id
        /// </summary>
        public async Task<Appointment?> GetByIdAsync(int id)
            => await _context.Appointments
                .Include(a => a.Customer)
                .Include(a => a.Mechanic)
                .Include(a => a.Vehicle)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(a => a.Id == id);

        /// <summary>
        /// добавить новую запись
        /// </summary>
        public async Task<Appointment> AddAsync(Appointment entity)
        {
            _context.Appointments.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// лбновить запись
        /// </summary>
        public async Task UpdateAsync(Appointment entity)
        {
            _context.Appointments.Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// удалить запись по Id
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var item = await _context.Appointments.FindAsync(id);
            if (item == null) return;

            _context.Appointments.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
