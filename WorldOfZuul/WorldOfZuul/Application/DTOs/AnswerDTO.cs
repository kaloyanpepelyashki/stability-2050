using System.Text.Json.Serialization;

namespace WorldOfZuul.DTOs;

public class AnswerDTO
{
    [JsonPropertyName("possible_answers")]
    public List<string> PossibleAnswers {get;set;}
    
    [JsonPropertyName("right_answer")]
    public string RightAnswer {get;set;}
}