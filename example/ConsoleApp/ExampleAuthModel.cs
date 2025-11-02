using PocketbaseNetSdk.Models.Base;
using System.Text.Json.Serialization;

namespace ConsoleApp
{
    public class ExampleAuthModel : BaseAuthModel
    {
        [JsonPropertyName("TestFeld")]
        public string? TestFeld { get; set; }
    }
}
