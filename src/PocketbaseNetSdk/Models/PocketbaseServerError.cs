using System.Text.Json.Serialization;

namespace PocketbaseNetSdk.Models
{
    public class PocketbaseServerError
    {
        [JsonPropertyName("status")]
        public int Status { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }
}
