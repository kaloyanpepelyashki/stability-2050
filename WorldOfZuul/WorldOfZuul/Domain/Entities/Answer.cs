namespace WorldOfZuul;

public class Answer
{
  public List<string> PossibleAnswers {get; set;}   
  public string RightAnswer {get; set;}

  public Answer(List<string> possibleAnswers, string rightAnswer)
  {
    PossibleAnswers = possibleAnswers;
    RightAnswer = rightAnswer;
  }
}