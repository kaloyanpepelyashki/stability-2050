using WorldOfZuul.Presentation.Console.Assets;
using System;

namespace WorldOfZuul.Presentation.Console.CLI;

public class QuizScreen
{
    public QuizScreen()
    {
        
    }
    
    /// <summary>
    /// Returns the possible answers associated with a question, as a parameter gets the question, and extracts the possible answer and concatinates them in a string.
    /// The method formats the possible answers, preparing the for display in the console. 
    /// </summary>
    /// <param name="question">The question which answers are to be extracted and formatted for display</param>
    /// <returns></returns>
    private string PossibleAnswers(Question question)
    {
        string text = question.QuestionText + "\n" + "possible answers:\n";
        
        for (int i = 0; i < question.Answers.PossibleAnswers.Count; i++)
        {
            text += "["+(i+1)+"] "+ question.Answers.PossibleAnswers[i]+"\n";
        }
        
        return text;
        
    }
    
    /// <summary>
    /// Displays the general quiz info. Additionally prompts the user to select an action
    /// </summary>
    /// <returns>String - the user response to the console prompt</returns>
    public string DisplayQuizInfo()
    {
        MenuText quizInfo = new MenuText("QUIZ OVERVIEW",
            "this region contains 6 dilemmas(questions). each question has 4 options\n"+
            "two options improve CPI and two damage CPI\n"+
            "IMPORTANT: once a question is shown you MUST answer it.\n"+
            "valid answers while a question is active: '1' ,'2', '3','4' \n You need to answer with a question from 1 to 4"+
            "while a question is active, you may type 'help' to view the help menu or 'status' to view current CPI values\n"+
            "but you CANNOT cancel the active question.\n"+
            "invalid inputs during an active question will re-prompt the same question.\n"+
            "after each answer the game displays immediate feedback (cpi change and short narration) and proceeds to the next question\n"+
            "at the end of the 6th question a short summary shows total CPI change and returns you to the regions menu"
            ,"continue or type 'cancel' to leave","introduction");
        
        return quizInfo.Display();
    }

    public void DisplayQuizTaken()
    {
        MenuText quizTaken = new MenuText("       QUIZ", "\n sorry you have done the quiz.\n", "return to continue", "quizDone");
        quizTaken.Display();
    }
    
    /// <summary>
    /// Displays a question associated with the quiz. 
    /// </summary>
    /// <param name="questionNumber">The index of the question in the queue of questions associated with the region</param>
    /// <param name="question">The question object, that holds data both about the question itself and about the answers</param>
    public string DisplayQuizQuestion(int questionNumber, Question question)
    {
        string possibleAnswers = PossibleAnswers(question);
        MenuText questionInfo = new MenuText("Question " + questionNumber.ToString(), possibleAnswers, null, "question");
        questionInfo.Display();
        
        string userInput = System.Console.ReadLine();
        return userInput;
    }

    public void DisplayCorrectAnswer(string regionName)
    {
        MenuText correctAnswerScreen = new MenuText("Question ", $"You answered correctly. Good choice. You have helped {regionName} become less corrupt The cpi of {regionName} increased", null, "correctAnswer");
        correctAnswerScreen.Display();
    }

    public void DisplayWrongAnswer(string regionName)
    {
        MenuText wrongAnswerScreen = new MenuText("Question ", $"You answered incorrectly. The cpi of {regionName} decreased.", null, "wrongAnswer");
        wrongAnswerScreen.Display();
    }

    public void DisplayQuizEnd()
    {
        MenuText displayQuizEndScreen = new MenuText("Quiz End", "Thank you for taking this quiz", "leave", "quizEnd");
        displayQuizEndScreen.Display();
    }
    
}