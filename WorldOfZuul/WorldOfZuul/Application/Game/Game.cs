using WorldOfZuul.Interfaces;
using WorldOfZuul.Presentation.Console.CLI;

namespace WorldOfZuul
{
    public class Game
    {
        private GameState _gameState = GameState.None;

        /// <summary>
        /// Tracks if the player has requested end of the game
        /// </summary>
        /// <remarks>
        /// Loop control variable
        /// </remarks>
        bool _continuePlaying = true;

        private RegionDataParser regionDataParser;

        // Tracks the room the player is currently in
        private Region? _currentRegion;

        // Stores the previous room, used when the player types 'back'
        private Region? _previousRegion;

        /// <summary>
        /// The turn counter instance. The turn counter is responsible for keeping up with how many turns the user has taken and how many turns are left, until the game is over.
        /// </summary>
        /// <remarks>
        /// The variable is readonly. 
        /// </remarks>
        private readonly TurnCounter _turnCounter;

        private IRegionsService _regionService;

        private CpiTracker _cpiTracker;

        private Dictionary<string, Region> _regions;

        private static ConsoleHandlerService _cli;

        private World? _world = null;

        private static GameScreen gameScreen;
        private static QuizScreen quizScreen;



        // Constructor - initializes the game world when a new Game object is created
        public Game(ConsoleHandlerService consoleHandler, IRegionsService regionsService, TurnCounter turnCounter,
            CpiTracker cpiTracker, World world)
        {
            try
            {

                _regionService = regionsService ?? throw new ArgumentNullException(nameof(regionsService));
                this._cpiTracker = cpiTracker ?? throw new ArgumentNullException(nameof(_cpiTracker));
                this._turnCounter = turnCounter ?? throw new ArgumentNullException(nameof(_turnCounter));
                _cli = consoleHandler ?? throw new ArgumentNullException(nameof(consoleHandler));
                _world = world ?? throw new ArgumentNullException(nameof(world));

                
                CreateRegions(); // Builds all regions 
                
                _turnCounter.AssignWorld(_world);
                gameScreen = new GameScreen(turnCounter, cpiTracker, this._currentRegion, null, world);
               
                
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Error. Couldn't construct game. {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error constructing Game. {e.Message}");
            }
        }

        /// <summary>
        /// The method is in charge of initialising all regions in the game, handles internally (with the use of another method), the read from JSON file, the object instantiation of each region and assignment of exits.
        /// The method also sets the initial region, where the player stars their journey
        /// </summary>
        private void CreateRegions()
        {
            try
            {
                _regions = _regionService.InitialiseRegions();

                //Sets the initial region to "North Africa"
                // Player starts the game in North Africa
                _currentRegion = _regions["North Africa"];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// The method is in charge of checking if the conditions for finalising the game are present
        /// The method checks if the player is out of turns. 
        /// </summary>
        private void CheckEndGame()
        {
            CheckPlayerTurnState();
            
            if (_cpiTracker.CheckWinCondition())
            {
                _gameState = GameState.PlayerWon;
            }

            if (!_continuePlaying)
            {
                _gameState = GameState.PlayerQuitGame;
            }
        }

        private void CheckPlayerTurnState()
        {
            
            bool crisisHasOccured = _cpiTracker.CheckCrisisCondition();
            _turnCounter.CheckOutOfTurns();

            if (_turnCounter.OutOfTurns)
            {
                _gameState = GameState.PlayerOutOfTurns;
            }

            if (_turnCounter.OutOfTurns && crisisHasOccured)
            {
                _gameState = GameState.PlayerCausedGlobalCrisis;
            }
        }

        
        /// <summary>
        /// In charge of checking if a global crisis has occured, and follows the necessary steps to handle the process
        /// around a global crisis.
        /// The method is intended to be called on the top of the Play loop. So it checks the global crisis state, after each re-run of the loop
        /// The method handles both the occurance of a crisis and the end of a such. The method checks if a global crisis is present, if yes, checks if last chance has not been initiated, and in case, it hasn't
        /// attempts to see if a last chance will be granted. In case a last chance is not granted, ends the game, by setting the game state.
        /// If a global crisis is not currently happening, checks if last chance is initiated, and in case it is, resets it.
        /// </summary>
        private void CheckCrisisState()
        {
            try
            {
                
                _cpiTracker.CheckCrisisCondition();
                bool globalCrisis = _world.GlobalCrisis;
                bool lastChanceInitiated = _turnCounter.LastChanceInitiated;

                if (globalCrisis)
                {

                    if (!lastChanceInitiated)
                    {
                        bool lastTurnGranted = _turnCounter.InitLastTwoTurns();

                        if (!lastTurnGranted)
                        {
                            _gameState = GameState.PlayerCausedGlobalCrisis;
                        }

                        _gameState = GameState.PlayerHasLastChance;
                    }
                }
                if (!globalCrisis)
                {
                    if (lastChanceInitiated)
                    {
                       _turnCounter.ResetLastTwoTurns();
                       _gameState = GameState.Running;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error checking crisis state: {e.Message}");
                throw;
            }
        }

        private void HandleQuiz()
        {
            QuizSession currentRegionQuizSession = _currentRegion.TakeRegionalQuiz();
            currentRegionQuizSession.LoadQuiz();
            double regionCpiBeforeQuiz = _currentRegion.RegionCpi;
            
            if (currentRegionQuizSession.QuizSessionCompleted)
            {
                //Gets the result of the quiz, after the quiz has been completed
                Dictionary<int, bool> quizResults = currentRegionQuizSession.QuizResults;

                foreach (KeyValuePair<int, bool> quizResult in quizResults)
                {
                    
                    //Handles the increase or decrease of CPI, based on the validity of the answer. If the answer is correct, it increases CPI, if the answer is incorrect, decreases CPI
                    if (quizResult.Value)
                    {
                        _cpiTracker.IncreaseCpi(_currentRegion);
                        
                    } else if (!quizResult.Value)
                    {
                        _cpiTracker.DecreaseCpi(_currentRegion);
                    }
                    
                }
                
                Console.WriteLine($"Cpi for {_currentRegion.RegionName} changed from {regionCpiBeforeQuiz}% to {_currentRegion.RegionCpi}%"); 
                Console.WriteLine("Press Enter to continue...");
                Console.ReadKey();
            }
            
        }
        
        /// <summary>
        /// A method encapsulating the logic for incrementing a turn and a year after a move has been made
        /// </summary>
        private void HandleMove()
        {
            _turnCounter.IncrementTurn();
        }
        
        
        //This method is to be used, to finalise the logic for turn increment
        private void HandleTurnIncrement()
        {
            bool playerOutOfTurns = _turnCounter.CheckOutOfTurns();

            if (!playerOutOfTurns)
            {
                _turnCounter.IncrementTurn();
            }

            _gameState = GameState.PlayerOutOfTurns;
        }

        private void DisplayStandardRoomOutput()
        {
            // Display current room's short description - the description associated with each of the rooms
            Console.WriteLine(_currentRegion?.RegionName);
            Console.Write("> ");
            Console.WriteLine($"The global CPI is {_cpiTracker.GlobalCpi}");

        }
        
        // Main method that runs the gameplay loop
        public void Play()
        {
            _gameState = GameState.Running;
            //Instantiating the parser class
            Parser parser = new(); // Responsible for interpreting player input

             //Prints the welcome message to the console
             PrintWelcome();
      
            // Main game loop - runs until player quits. 
            while (_gameState == GameState.Running || _gameState == GameState.PlayerHasLastChance)
            { 
                CheckCrisisState();
                CheckEndGame();
               
                
                DisplayStandardRoomOutput();
                
                gameScreen.Update(_currentRegion, _previousRegion);
                gameScreen.Display();
                
                //Gets the user command line input
                string? input = Console.ReadLine();
                
                // Checks for input validity and Handles empty input
                if (string.IsNullOrEmpty(input))
                {   
                   //Prompts the player to enter a command 
                    Console.WriteLine("Please enter a command.");
                    continue;
                }

                Command? command = parser.GetCommand(input);

                if (command == null)
                {
                    Console.WriteLine("I don't know that command.");
                    continue;
                }

                switch(command.Name)
                {
                    case "look":
                        Console.WriteLine(_currentRegion?.RegionDescription);
                        break;

                    case "back":
                        if (_previousRegion == null)
                            Console.WriteLine("You can't go back from here!");
                        else
                            _currentRegion = _previousRegion;
                        break;
                    
                    case "quiz":
                        HandleQuiz();
                        break;
                    
                    case "north":
                    case "south":
                    case "east":
                    case "west":
                        Move(command.Name);
                        break;

                    case "quit":
                        _continuePlaying = false;
                        break;

                    case "help":
                        PrintHelp();
                        break;
                    
                    case "leave":
                        gameScreen.Update(_currentRegion,_previousRegion);
                        gameScreen.hasMoved = true;
                        gameScreen.left = true;
                        break;
                        
                    default:
                        Console.WriteLine("Command unknown. Choose from available commands.");
                        break;
                }
            }
            
            _cli.HandleEndGame(_gameState);

        }
        
        /// <summary>
        /// The method is in charge of handling a move action initiated by the player
        /// THe method sets the _previousRegion to the current region and moves the _currentRegion to the direction chosen by the player
        /// The method also calls the HandleMove method, incrementing the year and the amount of taken turns
        /// </summary>
        /// <param name="direction">The direction the player chooses to move to</param>
        private void Move(string direction)
        {
            try
            {
                if (_currentRegion?.Exits.ContainsKey(direction) == true)
                {
                    _previousRegion = _currentRegion;
                    _currentRegion = _currentRegion?.Exits[direction];
                    HandleMove();
                }
                else
                {
                    Console.WriteLine($"You have come too far {direction}! Nowhere more to go in this direction.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Displays the welcome message shown at the start of the game.
        /// </summary>
        /// <remarks>
        /// This method introduces the player to the game and provides an initial
        /// description of what the game is about. It also calls <see cref="PrintHelp"/>
        /// to show the list of available commands immediately after the greeting.
        /// </remarks>
        private static void PrintWelcome()
        {
            _cli.Display("welcome");
        }
        
        /// <summary>
        /// Displays a list of available player commands and general guidance.
        /// </summary>
        /// <remarks>
        /// This method explains how the player can navigate through the game,
        /// view detailed room descriptions, go back to previous rooms,
        /// and quit the game.
        /// </remarks>
        private static void PrintHelp()
        {
            _cli.Display("help");
        }
    }
}
