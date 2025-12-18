using WorldOfZuul.Exceptions;
using WorldOfZuul.Presentation.Console.CLI;

namespace WorldOfZuul.Domain.Interfaces;

public interface IQuizzable
{
    public QuizSession TakeRegionalQuiz();
}