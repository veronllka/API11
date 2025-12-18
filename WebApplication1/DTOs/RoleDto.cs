namespace WebApplication1.DTOs
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = "";
    }

    public class CreateRoleDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = "";
    }

    public class UpdateRoleDto : CreateRoleDto { }
}
