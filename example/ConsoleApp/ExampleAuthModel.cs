using PocketbaseNetSdk.Models.Base;
using System.Text.Json.Serialization;

namespace ConsoleApp
{
    /// <summary>
    /// Custom authentication model extending BaseAuthModel.
    /// </summary>
    public class ExampleAuthModel : BaseAuthModel
    {
        [JsonPropertyName("TestFeld")]
        public string? TestFeld { get; set; }
    }
}
