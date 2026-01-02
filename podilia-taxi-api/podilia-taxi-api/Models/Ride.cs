
namespace podilia_taxi_api.Models
{
    public class Ride : IBaseEntity
    {
        public string Id { get; set; }
        public string PassengerId { get; set; }
        public User Passenger { get; set; }
        public string DriverId { get; set; }
        public User Driver { get; set; }
        public string PickupAddress { get; set; }
        public string DropoffAddress { get; set; }
        public double Price { get; set; }
        public double DistanceInKm { get; set; }
        public double DurationInMinutes { get; set; }
        public RideStatus Status { get; set; }
        public bool PaidPlatformFee { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
