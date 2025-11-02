using System.Text.Json.Serialization;

namespace PocketbaseNetSdk.Models
{
    public class PasswordLoginRequest
    {
        [JsonPropertyName("identity")]
        public string Identity { get; set; } = string.Empty;
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
    }
}
