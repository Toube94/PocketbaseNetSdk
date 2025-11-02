
using PocketbaseNetSdk.Models.Base;
using System.Text.Json.Serialization;

namespace ConsoleApp
{
    public class ExampleModel : BaseCollectionModel
    {
        [JsonPropertyName("Name")]
        public string? Name { get; set; }
    }
}
