using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace podilia_taxi_api.Models
{
    public class User : IdentityUser, IBaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public double Rating { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; } 
        [NotMapped]
        public string Role { get; set; }

        public bool CanAuthenticate()
        {
            return IsActive && !IsBlocked && DeletedAt == null;
        }
    }
}
