using System.Text.Json.Serialization;

namespace WhosInSpaceRP.Models
{
    public class AstrosData
    {
        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("people")]
        public List<Astronaut> People { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        public AstrosData()
        {
            People = new List<Astronaut>();
        }
    }
}
