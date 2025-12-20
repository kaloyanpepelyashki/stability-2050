using WorldOfZuul.Presentation.Console.Assets;
using System;

namespace WorldOfZuul.Presentation.Console.CLI;

public class QuizScreen
{
    /*
    Question question; // the current question;
    private int questionIndex; //which question are you currently on;
    
    private MenuText questionScreen;
    private MenuText quizIntroduction;
    private MenuText status;
    private MenuText quizAnswered;

    private bool quizActive;

    private int[] available = {0,1,2,3,4,5,6};
    
    private Region currentRegion;
    private CpiTracker cpiTracker;
    private TurnCounter turnCounter;
    private readonly GameScreen gameScreen;
    private double startCpi;
    public QuizScreen (Region currentRegion,GameScreen gameScreen, CpiTracker cpiTracker, TurnCounter turnCounter)
    {
        this.gameScreen = gameScreen; //included so the header from there can be fetched
        this.cpiTracker = cpiTracker;
        this.turnCounter = turnCounter;

        this.currentRegion = currentRegion;

        quizAnswered = new MenuText("       QUIZ", "\nsorry you have done the quiz.\n", "return to continue", "quizDone");
        
        status = new MenuText("status",CpiStatus(),"return to the quiz","status");

        questionScreen = new MenuText("", "", "", "");
        
        quizIntroduction = new MenuText("QUIZ OVERVIEW",
            "this region contains 6 dilemmas(questions). each question has 4 options\n"+
            "two options improve CPI and two damage CPI\n"+
            "IMPORTANT: once a question is shown you MUST answer it.\n"+
            "valid answers while a question is active: '1' ,'2', '3','4' \n"+
            "while a question is active, you may type 'help' to view the help menu or 'status' to view current CPI values\n"+
            "but you CANNOT cancel the active question.\n"+
            "invalid inputs during an active question will re-prompt the same question.\n"+
            "after each answer the game displays immediate feedback (cpi change and short narration) and proceeds to the next question\n"+
            "at the end of the 6th question a short summary shows total CPI change and returns you to the regions menu"
            ,"continue or type 'cancel' to leave","introduction");
    }

    private string CpiStatus()
    {
        string text = "Regional cpi for " + currentRegion.RegionName+": ";
        text += currentRegion.RegionCpi+"\n";
        text += "\nTotalCpi: " + cpiTracker.GlobalCpi;
        
        return text;
        
    }


    
    
    //TODO The quiz logic should be seprated from the Quiz UI. Preferably, the quizes should be implemented through an interface, as all quizzes follow the same structure, no matter of the answers/questions. Also, it will be easier to track if the player is currently in a quiz, if the player left the quiz, the player started the quiz or gave up on the quiz.
    public void Start(Region currentRegion)
    {
        
        if (currentRegion.QuizCompleted)
        {
            quizAnswered.Display();
            return;
        }
        
        this.currentRegion = currentRegion;

        startCpi = currentRegion.RegionCpi;
        
        questionIndex = 0;
        available = new int[currentRegion.Questions.Count];
        for (int i = 0; i < currentRegion.Questions.Count; i++)
        {
            available[i] = i;
        }
        
        
        //shuffle the available list so the numbers 0-currentRegion.Questions.Count appear in random order
        Random rand =  new Random();
        rand.Shuffle(available);

        int questionNumber = available[questionIndex];

        question = currentRegion.Questions[questionNumber];
        
        string userInput = quizIntroduction.Display() ?? "";
        userInput = userInput.Trim().ToLowerInvariant();
        
        if (userInput == "cancel")
        {
            turnCounter.IncrementTurn();
            //exit out of the quiz and return to game class
            return;
        }
        while (!quizActive)
        {
            questionScreen = new MenuText("Question "+(questionNumber+1),PossibleAnswers(),null,"question");
            questionScreen.Display();
            
            string? input = System.Console.ReadLine();
            
            if (input == null) return;
            
            String userAnswer = "";
            
            
            switch (input)
            {
                case "1":
                    userAnswer = question.Answers.PossibleAnswers[0];
                    break;
                case "2":
                    userAnswer = question.Answers.PossibleAnswers[1];
                    break;
                case "3":
                    userAnswer = question.Answers.PossibleAnswers[2];
                    break;
                case "4":
                    userAnswer = question.Answers.PossibleAnswers[3];
                    break;
                case "status":
                    status.Display();
                    continue;
                default:
                    System.Console.WriteLine("Input doesnt match the given answers");
                    System.Console.ReadKey();
                    continue;
            }
            
            if (!question.Answers.PossibleAnswers.Contains(userAnswer))
            {
                System.Console.WriteLine("Input doesnt match the given answers");
                continue;
            }

            userAnswer += "+"; //added so the userAnswer can match the right answer since the right answer for some reason has a plus added to the end
            turnCounter.IncrementTurn();
            if (question.Answers.RightAnswer == userAnswer)
            {
                System.Console.WriteLine("Good choice. You have helped "+currentRegion.RegionName+" become less corrupt.");
                //logic to add cpi from totalCpi
                cpiTracker.IncreaseCpi(currentRegion);
            }
            else
            {
                System.Console.WriteLine("Poor choice. You actions lowered "+currentRegion.RegionName+"'s CPI");
                //logic to subtract cpi from totalCpi
                cpiTracker.DecreaseCpi(currentRegion);
            }
            
            System.Console.Write(questionIndex+1);
            System.Console.WriteLine("/"+currentRegion.Questions.Count+" complete.");

            TextAssets.EnterPrompt("Continue to next answer");
            
            System.Console.WriteLine("");
            
            questionIndex++;
            
            if (questionIndex >= currentRegion.Questions.Count)
            {
                System.Console.WriteLine("Quiz complete. Total CPI change for " + currentRegion.RegionName+":");

                System.Console.WriteLine(currentRegion.RegionCpi-startCpi);
                
                System.Console.WriteLine("Starting CPI:");
                
                System.Console.WriteLine(startCpi);
                
                System.Console.WriteLine("Ending cpi: ");
                
                System.Console.WriteLine(currentRegion.RegionCpi);
                
                TextAssets.EnterPrompt("Return to the region menu");

                currentRegion.QuizCompleted = true;
                
                
                
                //end the quiz
                return;
            }
            questionNumber = available[questionIndex];
            question = currentRegion.Questions[questionNumber];
            System.Console.WriteLine();

        }
        
    }
    
    //!! ADDED NEW
    public void DisplayQuizTaken()
    {
        quizAnswered.Display();
        return;
    }
    */

    //TODO Re-Implement QuizScreen class and it's methods in a way, the presentation is seprate from the logic of handling the quiz itself.
    
    
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