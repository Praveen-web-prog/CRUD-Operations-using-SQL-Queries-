namespace DumpApplication.WebApi.Models
{
    public class UpdateEmployeeDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public decimal PhoneNumber { get; set; }
    }
}
