namespace WebApplication1.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        public string Brand { get; set; } = null!;      
        public string Model { get; set; } = null!;   
        public string PlateNumber { get; set; } = ""; 
        public int Year { get; set; }

        public int OwnerId { get; set; }
        public User Owner { get; set; } = null!;

        public List<Appointment> Appointments { get; set; } = new();
    }
}
