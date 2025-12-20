using WorldOfZuul.Exceptions;

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

    public bool Answer(int choice)
    {
        return Answers.PossibleAnswers[choice - 1] == Answers.RightAnswer;
    }

}