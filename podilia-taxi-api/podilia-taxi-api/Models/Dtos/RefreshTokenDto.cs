using System.ComponentModel.DataAnnotations;

namespace podilia_taxi_api.Models.Dtos
{
    public class RefreshTokenDto
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
