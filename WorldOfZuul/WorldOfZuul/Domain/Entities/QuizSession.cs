using WorldOfZuul.Exceptions;
using WorldOfZuul.Presentation.Console.CLI;

namespace WorldOfZuul;

public enum QuizStartChoice
{
   Continue,
   Cancel,
   Invalid,
}

public class QuizSession
{
   private Region _region;
   private double _startCpi;
   private QuizScreen _quizScreen;
   private bool _quizActive = true; 

   public QuizSession(Region region)
   {
      _region = region;
      _startCpi = _region.RegionCpi;
      _quizScreen = new QuizScreen();
   }
   
   /// <summary>
   /// Answers the question, takes input, from the user, that must come in the form of an integer. Compares the answer (answer index) to the right answer, if the answer is right, returns true, otherwise returns false.
   /// </summary>
   /// <param name="answeIndex"></param>
   /// <returns></returns>
   //TODO Figure out how to effectively integrate the logic with increasing and decreasing CPI of the region. That should happen through the CPI tracker. There must be an access point, where the quiz result accesses the CPI tracker. 
   public bool Answer(string answer, Question question)
   {
      int answerIndex = Convert.ToInt32(answer);

      bool isAnswerCorrect = question.Answer(answerIndex);
      
      return isAnswerCorrect;
   }

   public void LoadQuiz()
   {
      if (_region.QuizCompleted)
      {   
         _quizScreen.DisplayQuizTaken();
         return;
      }

      while (_quizActive)
      {
         string userInput = _quizScreen.DisplayQuizInfo();
         QuizStartChoice quizStartChoice = ParseQuizStartInput(userInput);

         switch (quizStartChoice)
         {
            case QuizStartChoice.Cancel:
               _quizActive = false;
               return;
            case QuizStartChoice.Continue:
               StartQuiz();
               return;
            case QuizStartChoice.Invalid:
               Console.WriteLine("Invalid input");
               break;
         }
      }
      
   }
   
   /// <summary>
   /// Parses the start input of the quiz. Returns a state from the QuizStartChoice enum
   /// </summary>
   /// <param name="input">The user start input to be parsed.</param>
   /// <returns>Continue, Cancel, Invalid</returns>
   private QuizStartChoice ParseQuizStartInput(string input)
   {
      // Handles ENTER only (no characters typed)
      if (input == "")
      {
         return QuizStartChoice.Continue;
      }

      // Spaces, tabs, etc. are INVALID
      if (string.IsNullOrWhiteSpace(input))
      {
         return QuizStartChoice.Invalid;
      }
      
      input = input.Trim().ToLower();
      
      //Handles in case the user typed in "continue" or "cancel"  
      return input switch
      {
         "continue" => QuizStartChoice.Continue,
         "cancel" => QuizStartChoice.Cancel,
         _ => QuizStartChoice.Invalid,
      };
   }
   
   /// <summary>
   /// Handels the display and answer of a question. Utilisies the QuizScreen class to display the question and take user input
   /// </summary>
   /// <param name="questionNumber">The number of the question in the region</param>
   /// <param name="question">The current region question the user has to answer</param>
   /// <returns></returns>
   //TODO Implement the logic for handling each of the quiz questions (each question of the region). The method should handle question display, and handle answer. Should return true, if the answer was answered correctly and false if the answer was not. 
   private bool HandleQuizQuestion(int questionNumber, Question question)
   {
      string userAnswer  = _quizScreen.DisplayQuizQuestion(questionNumber, question);
      
      Console.WriteLine($"User answer is {userAnswer}");
      bool isAnswerCorrect = Answer(userAnswer, question);

      return isAnswerCorrect;
   }

   
   //TODO Implement the logic for handling each question of the region- the logic for tracking which question is currently displayed and which question is being answered. Also the logic for tracking how many questions have been answered and how many questions are left to be answered. 
   public void StartQuiz()
   {
      
      int quizQuestionCount = _region.Questions.Count;
      int currentQuizQuestionIndex = 0;

      while (quizQuestionCount != currentQuizQuestionIndex)
      {
         int questionNumber = currentQuizQuestionIndex + 1;
         bool isAnswerCorrect = HandleQuizQuestion(questionNumber, _region.Questions[currentQuizQuestionIndex]);

         if (isAnswerCorrect)
         {
            _quizScreen.DisplayCorrectAnswer();
            Console.ReadKey();
         }
         else
         {
            Console.WriteLine("Wrong Answer"); 
            Console.ReadKey();
         }
         
         currentQuizQuestionIndex++; 
      }

      return; 
   }
}