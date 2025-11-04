namespace WorldOfZuul.Presentation.Console.CLI;

public class QuizScreen
{
    Question question; // the current question;
    private int questionIndex; //which question are you currently on;
    
    private MenuText questionScreen;
    private MenuText quizIntroduction;
    private MenuText status;

    private bool quizActive;

    private int[] available = {0,1,2,3,4,5,6};
    
    private Region currentRegion;
    
    private GameScreen gameScreen;
    private TurnCounter turnCounter;
    private double startCpi;
    public QuizScreen (Region currentRegion,GameScreen gameScreen)
    {
        this.gameScreen = gameScreen;//included so the header from there can be fetched
        
        this.currentRegion = currentRegion;
        
        status = new MenuText("status",CPIStatus(),"return to the quiz","status");

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

    public string CPIStatus()
    {
        string text = "Regional cpi for " + currentRegion.RegionName+": ";
        text += currentRegion.RegionCpi+"\n";
        text += "\nTotalCpi: " + gameScreen.cpiTracker.GlobalCpi;
        
        return text;
        
    }

    public string PossibleAnswers()
    {
        string text = question.QuestionText + "\n" + "possible answers:\n";
        
        for (int i = 0; i < question.Answers.PossibleAnswers.Count; i++)
        {
            text += "["+(i+1)+"] "+ question.Answers.PossibleAnswers[i]+"\n";
        }
        
        return text;
        
    }

    public void Start(Region currentRegion, TurnCounter turnCounter)
    {
        this.currentRegion = currentRegion;
        this.turnCounter = turnCounter;

        startCpi = currentRegion.RegionCpi;
        
        questionIndex = 0;
        available = new int[currentRegion.Questions.Count];
        for (int i = 0; i < currentRegion.Questions.Count; i++)
        {
            available[i] = i;
        }
        
        
        //shuffle the available list so the numbers 0-5 appear in random order
        Random rand =  new Random();
        rand.Shuffle(available);

        int questionNumber = available[questionIndex];

        question = currentRegion.Questions[questionNumber];
        
        string userInput =  quizIntroduction.Display();
        
        turnCounter.IncrementTurn();
        
        if (userInput == "cancel")
        {
            //gameScreen.currentTurn.IncrementTurn();
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
            
            if (question.Answers.RightAnswer == userAnswer)
            {
                System.Console.WriteLine("Good choice. You have helped "+currentRegion.RegionName+" become less corrupt.");
                //logic to add cpi from totalCpi
                gameScreen.cpiTracker.IncreaseCpi(currentRegion);
            }
            else
            {
                System.Console.WriteLine("Poor choice. You actions lowered "+currentRegion.RegionName+"'s CPI");
                //logic to subtract cpi from totalCpi
                gameScreen.cpiTracker.DecreaseCpi(currentRegion);
            }
            
            System.Console.Write(questionIndex+1);
            System.Console.WriteLine("/"+currentRegion.Questions.Count+" complete.");

            TextAssets.EnterPrompt("Continue to next answer");
            
            System.Console.WriteLine("");
            
            questionIndex++;
            
            if (questionIndex >= currentRegion.Questions.Count)
            {
                System.Console.WriteLine("Quiz complete. total CPI change for " + currentRegion.RegionName+":");
                
                //System.Console.WriteLine(currentRegion.RegionCpi);
                
                System.Console.WriteLine("Starting CPI:");
                
                System.Console.WriteLine(startCpi);
                
                //gameScreen.cpiTracker.IncreaseCpi(currentRegion,currentRegion.RegionCpi - startCpi);
                
                System.Console.WriteLine("Ending cpi: ");
                
                System.Console.WriteLine(currentRegion.RegionCpi); //globalCpi now contains Starting cpi + totalCpi

                TextAssets.EnterPrompt("Return to the region menu");
                
                gameScreen.currentTurn.IncrementTurn();
                
                //end the quiz
                return;
            }
            questionNumber = available[questionIndex];
            question = currentRegion.Questions[questionNumber];
            System.Console.WriteLine();

        }
        
    }
    
}