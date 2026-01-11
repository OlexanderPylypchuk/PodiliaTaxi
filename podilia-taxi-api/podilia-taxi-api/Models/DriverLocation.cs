namespace podilia_taxi_api.Models
{
    public class DriverLocation
    {
        public string DriverId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Heading { get; set; }
        public double Speed { get; set; }
        public DateTime RecordedAt { get; set; }
    }
}
