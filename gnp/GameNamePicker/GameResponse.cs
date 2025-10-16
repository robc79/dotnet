using System.Text.Json.Serialization;

namespace GameNamePicker;

public class GameResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}