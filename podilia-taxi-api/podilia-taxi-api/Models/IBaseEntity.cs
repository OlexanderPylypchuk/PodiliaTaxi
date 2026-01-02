namespace podilia_taxi_api.Models
{
    public interface IBaseEntity //allows tracking of entity lifecycle events, also allows for soft deletion
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
