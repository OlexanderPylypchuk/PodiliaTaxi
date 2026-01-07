using podilia_taxi_api.Utility;
using System.ComponentModel.DataAnnotations;

namespace podilia_taxi_api.Models.Dtos
{
    public class RegistrationDto
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Phone { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        public string Role { get; set; } = SD.Role_Customer;
    }
}
