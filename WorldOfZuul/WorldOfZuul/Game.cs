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

        private Dictionary<string, Region> regions;
        
        private Menutext Welcome;
        
        // Constructor - initializes the game world when a new Game object is created
        public Game(IRegionsService regionsService, TurnCounter turnCounter)
        {   
            //TODO, change, the region service must be dependency injected
            _regionService =  regionsService;
            
            turnCounter = turnCounter;
            CreateRooms(); // Build all rooms and set up exits
            
            Welcome = new Menutext("WELCOME TO STABILITY 2050",
                "Stabilty 2050 is a text based strategic game.\n" +
                "You are in a position of a diplomat,\n" +
                "who is trying to fight corruption.\n" +
                "Every action changes CPI - the measure of global trust.\n" +
                "Your goal is to lead humanity to corruption-free world by 2050.\n \n " +
                "YOUR MISSION:\n" +
                "Reduce corruption and strengthen institutions worldwide.\n" +
                "Every year represents one turn and you have 25 years to raise the Global CPI\n" +
                "to 80 or higher before 2050.\n" +
                "Your actions will affect both regional and global CPI levels.\n \n " +
                "HOW IT WORKS:\n" +
                "- You start with 4 regions, each with its own CPI value.\n" +
                "- Every turn, you’ll face a corruption-related question.\n" +
                "- Your actions can increase or decrease the CPI.\n" +
                "- The Global CPI is the average of all regional CPIs.\n" +
                "- If it drops below 20, the world enters a corruption crisis.\n " +
                "You’ll have 5 turns to recover, or the world collapses.\n" +
                "- If you reach CPI of 80 or more, you win immediately."+
                "AVAILABLE COMMANDS:\n" +
                "- Type a number to choose an action.\n" +
                "- Type 'west','east','north' or 'south' to travel to another region.\n" +
                "- Type 'help' for assistance.\n" +
                "- Type 'quit' to end the simulation.",
                "begin","welcome");
            
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

            Welcome.display(); //Prints the welcome message to the console
            
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
