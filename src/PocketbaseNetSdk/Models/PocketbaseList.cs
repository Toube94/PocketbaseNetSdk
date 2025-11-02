using PocketbaseNetSdk.Models.Base;
using System.Text.Json.Serialization;

namespace PocketbaseNetSdk.Models
{
    public class PocketbaseList<T> where T : BaseModel
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }
        [JsonPropertyName("perPage")]
        public int PerPage { get; set; }
        [JsonPropertyName("totalItems")]
        public int TotalItems { get; set; }
        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("items")]
        public List<T> Items { get; set; } = new();
    }
}
