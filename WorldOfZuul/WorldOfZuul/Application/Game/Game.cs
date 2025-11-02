using WorldOfZuul.Interfaces;

namespace WorldOfZuul
{
    public class Game
    {
        /// <summary>
        /// Keeps track of the game state. Has the game ended.
        /// If the value is set to true, the game automatically ends.
        /// </summary>
        private bool _gameHasEnded = false;
        
        /// <summary>
        /// Keeps track of whether the player lost or won the game
        /// It is used for when the game is about to end - both when the player has run out of turns, when the player has reached a crisis or when the player has fulfiled the winning condition of the game
        /// </summary>
        private bool _playerWonGame = false;
        
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
        private TurnCounter _turnCounter;

        private IRegionsService _regionService;
        
        private CpiTracker _cpiTracker;

        private Dictionary<string, Region> _regions;

        private static ConsoleHandlerService _cli;
        
        private static GameScreen gameScreen;
        
        // Constructor - initializes the game world when a new Game object is created
        public Game(ConsoleHandlerService consoleHandler, IRegionsService regionsService, TurnCounter turnCounter, CpiTracker cpiTracker)
        {   
            
            _regionService =  regionsService;
            this._cpiTracker= cpiTracker;
            this._turnCounter = turnCounter;
            _cli = consoleHandler;
            
            CreateRegions(); // Builds all regions 
            gameScreen = new GameScreen(this._turnCounter, this._cpiTracker, this._currentRegion,null);

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

                foreach (KeyValuePair<string, Region> region in _regions )
                {
                    Console.WriteLine(region.Key);
                }
                
                
                //Sets the initial region to "North Africa"
                // Player starts the game in North Africa
                _currentRegion = _regions["North Africa"];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void CheckEndGame()
        {
            bool isPlayerOutOfTurns = _turnCounter.OutOfTurns;

            if (isPlayerOutOfTurns)
            {
                _gameHasEnded = true;
                _playerWonGame = false;
            }
        }

        private void HandleEndGame()
        {
            if (_gameHasEnded)
            {
                Console.WriteLine("Game Over. Out of turns. Better Luck Next time");
                
            } else if (!_continuePlaying)
            {
                Console.WriteLine("Thank you for playing World of Zuul!");
            }
        }
        
        // Main method that runs the gameplay loop
        public void Play()
        {
            //Instantiating the parser class
            Parser parser = new(); // Responsible for interpreting player input

             //Prints the welcome message to the console
             PrintWelcome();
            
            // Main game loop - runs until player quits. 
            while (_continuePlaying && !_gameHasEnded && !_playerWonGame)
            {   
                
                // Display current room's short description - the description associated with each of the rooms
                Console.WriteLine(_currentRegion?.RegionName);
                Console.Write("> ");
                Console.WriteLine($"The global CPI is {_cpiTracker.GlobalCpi}");
                
                gameScreen.update(_currentRegion, _previousRegion);
                gameScreen.display();
                
                // TODO: Uncomment after implemented crisis system
                // cpiTracker.CheckCrisisCondition();
                
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

                    default:
                        Console.WriteLine("I don't know what command.");
                        break;
                }
            }
            
            HandleEndGame();

        }

        private void Move(string direction)
        {
            if (_currentRegion?.Exits.ContainsKey(direction) == true)
            {
                _previousRegion = _currentRegion;
                _currentRegion = _currentRegion?.Exits[direction];
            }
            else
            {
                Console.WriteLine($"You have come too far {direction}! Nowhere more to go in this direction.");
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
            _cli.display("welcome");
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
            _cli.display("help");
        }
    }
}
