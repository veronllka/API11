namespace WebApplication1.DTOs
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string PlateNumber { get; set; } = "";
        public int Year { get; set; }

        public int OwnerId { get; set; }
        public string OwnerName { get; set; } = "";
    }

    public class CreateVehicleDto
    {
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string PlateNumber { get; set; } = "";
        public int Year { get; set; }
        public int OwnerId { get; set; }
    }

    public class UpdateVehicleDto : CreateVehicleDto { }
}
