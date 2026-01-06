namespace podilia_taxi_api.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        public DateTime? RevokedAt { get; set; }
        public bool IsActive => RevokedAt == null && !IsExpired;
        public string? ReplacedByToken { get; set; }
        public string UserId { get; set; }
        public User User { get; set; } = null!;
    }

}
