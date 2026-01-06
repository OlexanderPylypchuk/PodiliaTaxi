using System.Security.Cryptography;

namespace podilia_taxi_api.Services
{
    public class HashService
    {
        public string Hash(string input)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            var hashBytes = SHA256.HashData(bytes);
            return Convert.ToBase64String(hashBytes);
        }   
    }
}
