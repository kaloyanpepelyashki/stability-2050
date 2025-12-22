using WorldOfZuul.Presentation.Console.Assets;
using System;

namespace WorldOfZuul.Presentation.Console.CLI;

public class QuizScreen
{
    public QuizScreen()
    {
        
    }
    
    /// <summary>
    /// Writes the possible answers for a question to the console with colored indexes.
    /// </summary>
    /// <param name="question">The question which answers are to be extracted and formatted for display</param>
    private void PrintPossibleAnswers(Question question)
    {
        System.Console.WriteLine(question.QuestionText);
        System.Console.WriteLine("possible answers:");
        for (int i = 0; i < question.Answers.PossibleAnswers.Count; i++)
        {
            System.Console.ForegroundColor = ConsoleColor.Cyan;
            System.Console.Write("[");
            System.Console.Write(i + 1);
            System.Console.Write("] ");
            System.Console.ResetColor();
            System.Console.WriteLine(question.Answers.PossibleAnswers[i]);
        }
    }
    
    /// <summary>
    /// Displays the general quiz info. Additionally prompts the user to select an action
    /// </summary>
    /// <returns>String - the user response to the console prompt</returns>
    public string DisplayQuizInfo()
    {
        MenuText quizInfo = new MenuText("QUIZ OVERVIEW",
            "This region contains 6 dilemmas (questions). Each question has 4 possible option.\n"+
            "Some options improve the CPI, and two decrease CPI.\n"+
            "\n"+
            "!!!! IMPORTANT: once a question is shown you MUST answer it !!!!\n"+
            "- Valid answers while a question is active: '1' ,'2', '3','4' \n"+
            "- You need to answer with a number range 1 to 4\n"+
            "- While a question is active, you may type 'help' to view the help menu or 'status' to view current CPI values\n"+
            "- You CANNOT cancel the active question\n"+
            "- After each answer, the game will give you immediate feedback (cpi change and short narration)\n"+
            "- After answering all questions, a short summary shows total CPI change and returns you to the Region menu"
            ,"continue or type 'cancel' to leave","introduction");
        
        return quizInfo.Display();
    }

    public void DisplayQuizTaken()
    {
        MenuText quizTaken = new MenuText("       QUIZ", "\n You have done this quiz.\n", "return to continue", "quizDone");
        quizTaken.Display();
    }
    
    /// <summary>
    /// Displays a question associated with the quiz. 
    /// </summary>
    /// <param name="questionNumber">The index of the question in the queue of questions associated with the region</param>
    /// <param name="question">The question object, that holds data both about the question itself and about the answers</param>
    public string DisplayQuizQuestion(int questionNumber, Question question)
    {
        System.Console.Clear();
        TextAssets.Header("Question " + questionNumber.ToString());
        PrintPossibleAnswers(question);
        
        string userInput = System.Console.ReadLine();
        return userInput;
    }

    public void DisplayCorrectAnswer(string regionName)
    {
        MenuText correctAnswerScreen = new MenuText("Question ", $"Good choice! You have helped {regionName} become less corrupt. The CPI of {regionName} increased", null, "correctAnswer");
        correctAnswerScreen.Display();
    }

    public void DisplayWrongAnswer(string regionName)
    {
        MenuText wrongAnswerScreen = new MenuText("Question ", $"You answered incorrectly. The CPI of {regionName} decreased.", null, "wrongAnswer");
        wrongAnswerScreen.Display();
    }

    public void DisplayQuizEnd()
    {
        MenuText displayQuizEndScreen = new MenuText("Quiz End", "Thank you for taking this quiz", "leave", "quizEnd");
        displayQuizEndScreen.Display();
    }
}
