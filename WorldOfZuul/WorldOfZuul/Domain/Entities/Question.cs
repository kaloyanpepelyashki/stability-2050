namespace WorldOfZuul;

public class Question
{
    public string QuestionText {get; set;}
    public Answer Answers {get; set;}

    public Question(string questionText, List<string> possibleAnswers, string rightAnswer)
    {
        QuestionText = questionText;
        Answers = new Answer(possibleAnswers, rightAnswer);
    }
}