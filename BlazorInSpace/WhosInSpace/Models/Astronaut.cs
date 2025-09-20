using System.Text.Json.Serialization;

namespace WhosInSpace.Models
{
    public class Astronaut
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("craft")]
        public string? Craft { get; set; }
    }
}
