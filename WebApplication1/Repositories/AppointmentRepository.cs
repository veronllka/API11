using Microsoft.EntityFrameworkCore;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly UsersDBContext _context;

        public AppointmentRepository(UsersDBContext context)
        {
            _context = context;
        }

        public async Task<List<Appointment>> GetAllAsync()
            => await _context.Appointments
                .Include(a => a.Customer)
                .Include(a => a.Mechanic)
                .Include(a => a.Vehicle)
                .Include(a => a.Service)
                .ToListAsync();

        public async Task<Appointment?> GetByIdAsync(int id)
            => await _context.Appointments
                .Include(a => a.Customer)
                .Include(a => a.Mechanic)
                .Include(a => a.Vehicle)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(a => a.Id == id);

        public async Task<Appointment> AddAsync(Appointment entity)
        {
            _context.Appointments.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Appointment entity)
        {
            _context.Appointments.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.Appointments.FindAsync(id);
            if (item == null) return;

            _context.Appointments.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
