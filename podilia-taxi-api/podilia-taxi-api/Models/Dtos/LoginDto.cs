using System.ComponentModel.DataAnnotations;

namespace podilia_taxi_api.Models.Dtos
{
    public class LoginDto
    {
        [Required]
        public string UserNameOrEmail { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
