namespace podilia_taxi_api.Models.Dtos
{
    public class UserDto
    {
        public string Id { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Password { get; set; } = null;
        public string Role { get; set; } = null!;
    }
}
