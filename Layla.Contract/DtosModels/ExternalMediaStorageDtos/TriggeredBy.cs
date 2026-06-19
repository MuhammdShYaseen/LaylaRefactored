using System.Text.Json.Serialization;

namespace Layla.Models.DtosModels.ExternalMediaStorageDtos
{
    public class TriggeredBy
    {
        [JsonPropertyName("source")]
        public string? Source { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }
    }
}
