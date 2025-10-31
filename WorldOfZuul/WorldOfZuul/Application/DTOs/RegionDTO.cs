using System.Text.Json.Serialization;

namespace WorldOfZuul.DTOs;

/// <summary>
/// Data transfer object. The mediator between the data in the JSON file and the data in the application
/// </summary>
public class RegionDTO
{   
    [JsonPropertyName("region_name")]
    public string RegionName { get; set; }
    
    [JsonPropertyName("region_description")]
    public string RegionDescription { get; set; }
    
    [JsonPropertyName("cpi")]
    public double RegionCPI {get; set;}
    
    [JsonPropertyName("state")]
    public StateDTO State { get; set; }
    
    [JsonPropertyName("exits")] 
    public ExitsDTO Exits { get; set; }
}