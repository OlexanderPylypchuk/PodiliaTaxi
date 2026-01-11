
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
        public int? PassengerRating { get; set; }
        public double PickupLatitude { get; set; }
        public double PickupLongitude { get; set; }

        public double DropoffLatitude { get; set; }
        public double DropoffLongitude { get; set; }
        public string? PassengerComment { get; set; }
        public int? DriverRating { get; set; }
        public string? DriverComment { get; set; }
        public string? RoutePolyline { get; set; }
        public double DistanceInKm { get; set; }
        public double DurationInMinutes { get; set; }
        public RideStatus Status { get; set; }
        public CurrencyCode Currency { get; set; }
        public bool PaidPlatformFee { get; set; }
        public string RideOrderId { get; set; }
        public RideOrder RideOrder { get; set; }
        public DateTime? AcceptedAt { get; set; }
        public DateTime? DriverArrivedAt { get; set; }
        public DateTime? CancelledAt { get; set; }
        public string? CancellationReason { get; set; }
        public string? CancelledByUserId { get; set; } // User who cancelled the ride
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
