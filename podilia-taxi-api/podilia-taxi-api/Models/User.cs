using Microsoft.AspNetCore.Identity;

namespace podilia_taxi_api.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public double Rating { get; set; }
    }
}
