using System.Text.Json.Serialization;

namespace PocketbaseNetSdk.Models.Base
{
    public class BaseAuthModel : BaseModel
    {
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("emailVisibility")]
        public bool? EmailVisibility { get; set; }

        [JsonPropertyName("verified")]
        public bool? Verified { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [JsonPropertyName("passwordConfirm")]
        public string? PasswordConfirm { get; set; }
    }
}
