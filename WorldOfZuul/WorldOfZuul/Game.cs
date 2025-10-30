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

        private RegionsService _regionService;

        private Dictionary<string, Region> regions;
        
        // Constructor - initializes the game world when a new Game object is created
        public Game()
        {   
            //TODO, change, the region service must be dependency injected
            _regionService = new RegionsService();
            
            turnCounter = TurnCounter.GetInstance();
            CreateRooms(); // Build all rooms and set up exits
        }
        
        // Creates all rooms and defines how they connect to each other
        private void CreateRooms()
        {
            try
            {
                regions = _regionService.InitialiseRegions();
                
               

 
                //Sets the initial room to "outside"
                // Player starts the game outside
                currentRegion = regions["Central Europe"];
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

            PrintWelcome(); //Prints the welcome message to the console
            
            //Loop control variable 
            bool continuePlaying = true; //Tracks if the player has requested a stop of the game
            
            // Main game loop - runs until player quits. 
            while (continuePlaying)
            {   
                // Display current room's short description - the descirption associated with each of the rooms
                Console.WriteLine(currentRegion?.RegionName);
                Console.Write("> ");
                
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
            Console.WriteLine("Welcome to the World of Zuul!");
            Console.WriteLine("World of Zuul is a new, incredibly boring adventure game.");
            PrintHelp();
            Console.WriteLine();
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
            Console.WriteLine("You are lost. You are alone. You wander");
            Console.WriteLine("around the university.");
            Console.WriteLine();
            Console.WriteLine("Navigate by typing 'north', 'south', 'east', or 'west'.");
            Console.WriteLine("Type 'look' for more details.");
            Console.WriteLine("Type 'back' to go to the previous room.");
            Console.WriteLine("Type 'help' to print this message again.");
            Console.WriteLine("Type 'quit' to exit the game.");
        }
    }
}
