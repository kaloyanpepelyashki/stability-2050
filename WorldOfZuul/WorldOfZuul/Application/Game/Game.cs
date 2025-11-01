using WorldOfZuul.Interfaces;

namespace WorldOfZuul
{
    public class Game
    {   
        private RegionDataParser regionDataParser;
        // Tracks the room the player is currently in
        private Region? currentRegion;
        // Stores the previous room, used when the player types 'back'
        private Region? previousRegion;

        private TurnCounter turnCounter;

        private IRegionsService _regionService;
        private CpiTracker cpiTracker;

        private Dictionary<string, Region> regions;

        private static ConsoleHandler CLI;
        
        private static GameScreen gameScreen;
        
        // Constructor - initializes the game world when a new Game object is created
        public Game(IRegionsService regionsService, TurnCounter turnCounter, CpiTracker cpiTracker)
        {   
            //TODO, change, the region service must be dependency injected
            _regionService =  regionsService;
            this.cpiTracker= cpiTracker;
            this.turnCounter = turnCounter;
            CreateRooms(); // Build all rooms and set up exits

            CLI = new ConsoleHandler();
            
            gameScreen = new GameScreen(turnCounter, cpiTracker,currentRegion,null);

        }
        
        /// <summary>
        /// The method is in charge of initialising all regions in the game, handles internally (with the use of another method), the read from JSON file, the object instantiation of each region and assignment of exits.
        /// The method also sets the initial region, where the player stars their journey
        /// </summary>
        private void CreateRooms()
        {
            try
            {
                regions = _regionService.InitialiseRegions();
                
                //Sets the initial room to "outside"
                // Player starts the game outside
                currentRegion = regions["North Africa"];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        // Main method that runs the gameplay loop
        public void Play()
        {
            //Instantiating the parser class
            Parser parser = new(); // Responsible for interpreting player input

             //Prints the welcome message to the console
            PrintWelcome();
            //Loop control variable 
            bool continuePlaying = true; //Tracks if the player has requested a stop of the game
            
            // Main game loop - runs until player quits. 
            while (continuePlaying)
            {   
                
                // Display current room's short description - the description associated with each of the rooms
                Console.WriteLine(currentRegion?.RegionName);
                Console.Write("> ");
                Console.WriteLine($"The global CPI is {cpiTracker.GlobalCpi}");
                
                gameScreen.update(currentRegion,previousRegion);
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
                        Console.WriteLine(currentRegion?.RegionDescription);
                        break;

                    case "back":
                        if (previousRegion == null)
                            Console.WriteLine("You can't go back from here!");
                        else
                            currentRegion = previousRegion;
                        break;

                    case "north":
                    case "south":
                    case "east":
                    case "west":
                        Move(command.Name);
                        break;

                    case "quit":
                        continuePlaying = false;
                        break;

                    case "help":
                        PrintHelp();
                        break;

                    default:
                        Console.WriteLine("I don't know what command.");
                        break;
                }
            }

            Console.WriteLine("Thank you for playing World of Zuul!");
        }

        private void Move(string direction)
        {
            if (currentRegion?.Exits.ContainsKey(direction) == true)
            {
                previousRegion = currentRegion;
                currentRegion = currentRegion?.Exits[direction];
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
            CLI.display("welcome");
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
            CLI.display("help");
        }
    }
}
