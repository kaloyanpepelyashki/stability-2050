using System.Text.Json.Serialization;

namespace WorldOfZuul.DTOs;

public class ExitsDTO
{   
    [JsonPropertyName("north")]
    public string North {get; set;}
    
    [JsonPropertyName("east")]
    public string East {get; set;}
    
    [JsonPropertyName("south")]
    public string South {get; set;}
    
    [JsonPropertyName("west")]
    public string West {get; set;}
}