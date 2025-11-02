using PocketbaseNetSdk.Models.Base;
using System.Text.Json.Serialization;

namespace PocketbaseNetSdk.Models
{
    public class TokenResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;
        [JsonPropertyName("record")]
        public BaseAuthModel Record { get; set; } = new BaseAuthModel();
    }
}
