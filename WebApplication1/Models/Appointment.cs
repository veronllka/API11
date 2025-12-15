namespace WebApplication1.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string Status { get; set; } = "Planned"; 
        public string? Comment { get; set; }

        public int CustomerId { get; set; }
        public User Customer { get; set; } = null!;

        public int MechanicId { get; set; }
        public User Mechanic { get; set; } = null!;

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = null!;

        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;
    }
}
