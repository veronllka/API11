namespace WebApplication1.DTOs
{
    public class ServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = "";
        public int DurationMinutes { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateServiceDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = "";
        public int DurationMinutes { get; set; }
        public decimal Price { get; set; }
    }

    public class UpdateServiceDto : CreateServiceDto { }
}
