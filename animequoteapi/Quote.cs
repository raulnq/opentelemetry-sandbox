using System.Text.Json.Serialization;

namespace AnimeQuoteApi;

public class Quote
{
    [JsonPropertyName("anime")]
    public string? Anime { get; set; }
    [JsonPropertyName("character")]
    public string? Character { get; set; }
    [JsonPropertyName("quote")]
    public string? Text { get; set; }
}
