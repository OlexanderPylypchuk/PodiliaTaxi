
namespace podilia_taxi_api.Models
{
    public class RideOrder : IBaseEntity
    {
        public string Id { get; set; }

        public string PassengerId { get; set; }
        public User Passenger { get; set; }
        public string PickupAddress { get; set; }
        public string DropoffAddress { get; set; }
        public double EstimatedPrice { get; set; }
        public double EstimatedDistanceInKm { get; set; }
        public double EstimatedDurationInMinutes { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
