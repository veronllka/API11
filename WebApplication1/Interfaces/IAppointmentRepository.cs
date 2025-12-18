using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    /// <summary>
    /// репозиторий записей на обслуживание
    /// </summary>
    public interface IAppointmentRepository : IRepository<Appointment>
    {
    }
}
