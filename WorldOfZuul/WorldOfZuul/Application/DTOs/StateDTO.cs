using System.Text.Json.Serialization;

namespace WorldOfZuul.DTOs;

/// <summary>
/// Data transfer object. The mediator between the data in the JSON file and the data in the application.
/// This class is in charge of handling the data between the state object in JSON and the State class
/// </summary>
public class StateDTO
{
    [JsonPropertyName("state_name")]
    public string StateName { get; set; }
    
    [JsonPropertyName("state_description")]
    public string StateDescription { get; set; }
}