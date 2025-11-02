using PocketbaseNetSdk.Json;
using System.Text.Json.Serialization;

namespace PocketbaseNetSdk.Models.Base
{
    public abstract class BaseModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        [JsonPropertyName("created")]
        [JsonConverter(typeof(DateTimeConverter))]
        public virtual DateTime? Created { get; set; }

        [JsonPropertyName("updated")]
        [JsonConverter(typeof(DateTimeConverter))]
        public virtual DateTime? Updated { get; set; }
    }
}
