namespace podilia_taxi_api.Models
{
    public class Rating
    {
        public string RaterId { get; set; }
        public User Rater { get; set; }
        public string RatedId { get; set; }
        public User Rated { get; set; }
        public double Score { get; set; }
    }
}
