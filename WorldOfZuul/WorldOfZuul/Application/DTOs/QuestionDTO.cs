using System.Text.Json.Serialization;

namespace WorldOfZuul.DTOs;

public class QuestionDTO
{   
    [JsonPropertyName("question_text")]
    public string QuestionText { get; set; }    
    
    [JsonPropertyName("answers")]
    public AnswerDTO Answers { get; set; }
}