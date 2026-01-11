
namespace podilia_taxi_api.Models
{
    public class RideOrder : IBaseEntity
    {
        public string Id { get; set; }

        public string PassengerId { get; set; }
        public User Passenger { get; set; }
        public string PickupAddress { get; set; }
        public string DropoffAddress { get; set; }
        public decimal BaseFare { get; set; }
        public decimal PricePerKm { get; set; }
        public decimal PricePerMinute { get; set; }
        public decimal SurgeMultiplier { get; set; }
        public decimal PlatformFee { get; set; }
        public decimal DriverEarnings { get; set; }
        public decimal Price { get; set; }
        public double EstimatedDistanceInKm { get; set; }
        public double EstimatedDurationInMinutes { get; set; }

        public double PickupLatitude { get; set; }
        public double PickupLongitude { get; set; }

        public double DropoffLatitude { get; set; }
        public double DropoffLongitude { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
