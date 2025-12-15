namespace WebApplication1.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
        public List<Vehicle> Vehicles { get; set; } = new();

        public List<Appointment> CustomerAppointments { get; set; } = new();
        public List<Appointment> MechanicAppointments { get; set; } = new();

    }
}
