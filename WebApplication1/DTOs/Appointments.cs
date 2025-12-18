namespace WebApplication1.DTOs
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; } = "Planned";
        public string? Comment { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = "";

        public int MechanicId { get; set; }
        public string MechanicName { get; set; } = "";

        public int VehicleId { get; set; }
        public string VehicleInfo { get; set; } = "";   

        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = "";
    }

    public class CreateAppointmentDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; } = "Planned";
        public string? Comment { get; set; }

        public int CustomerId { get; set; }
        public int MechanicId { get; set; }
        public int VehicleId { get; set; }
        public int ServiceId { get; set; }
    }

    public class UpdateAppointmentDto : CreateAppointmentDto { }
}
