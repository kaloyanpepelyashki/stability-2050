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

    public bool Answer(int answerIndex)
    {
        try
        {
            if (answerIndex < 0 | answerIndex > 4)
            {
                throw new QuizAnswerException("The answer is out of bounds.");
            }
          
            string answer = Answers.PossibleAnswers[answerIndex - 1];

            if (answer == Answers.RightAnswer)
            {
                return true;
            }

            return false; 
        }
        catch (QuizAnswerException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error answering question: {e.Message}");
            throw;
        } 
    }
}