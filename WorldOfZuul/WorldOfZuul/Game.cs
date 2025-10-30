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
        
        private static Menutext Welcome;
        private static Menutext Help;
        private static Menutext CPI;
        private static Menutext Goal;
        private static Menutext BasicCommands;
        private static Menutext GameStructure;
        private static Menutext ProTip;
        
        // Constructor - initializes the game world when a new Game object is created
        public Game(IRegionsService regionsService, TurnCounter turnCounter, CpiTracker cpiTracker)
        {   
            //TODO, change, the region service must be dependency injected
            _regionService =  regionsService;
            this.cpiTracker= cpiTracker;
            this.turnCounter = turnCounter;
            CreateRooms(); // Build all rooms and set up exits

            ProTip = new Menutext("help[4] - Pro tip",
                "Pro tip:\nCorruption spreads fast. Honesty takes time.\nChoose actions that build long-term integrity, not quick wins.",
                "return to help menu", "Pro Tip");
            
            GameStructure = new Menutext("help[3] - Game Structure","GAME STRUCTURE:\n- Each turn represents one year.\n- Each region’s CPI changes based on your decision.\n- The Global CPI is the average of all four regions.\n- A higher CPI means lower corruption.","return to help menu","Game Structure");
            
            CPI = new Menutext("Help[0] - CPI ",
                "CPI (Corruption Perception Index):\n- Represents how clean a region is.\n- Range: 0 = totally corrupt, 100 = fully transparent.",
                "return to the help menu", "CPI");
            
            Goal = new Menutext("help[1] - goal","GOAL:\n- Reach a global CPI of 80 before the year 2050.\n- If global CPI falls below 20, a corruption crisis begins.\n You’ll have 5 turns to recover","return to help menu", "Goal");
            
            BasicCommands = new Menutext("help[2] - Basic Commands","BASIC COMMANDS:\n- [number] → choose your response to a dilemma.\n- 'north','west','east' or 'south' → travel to another region.\n- help → show this help menu.\n- quit → exit the simulation.","return to help menu", "Basic Commands");

            Help = new Menutext("HELP MENU", null,null, "help",new Menutext[]{CPI,Goal,BasicCommands,GameStructure,ProTip},"Choose a number to read more:");

            Welcome = new Menutext("STABILITY 2050",
                "Stabilty 2050 is a text based strategic game.\nYou are in a position of a diplomat,\nwho is trying to fight corruption.\nEvery action changes CPI - the measure of global trust.\nYour goal is to lead humanity to corruption-free world by 2050.\n",
                null, null,new Menutext[] {Help}, "Type '0' to learn how to play or press ENTER to continue.");

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
                // Display current room's short description - the description associated with each of the rooms
                Console.WriteLine(currentRegion?.RegionName);
                Console.Write("> ");
                Console.WriteLine($"The global CPI is {cpiTracker.GlobalCpi}");
                
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
            Help.display();
        }
    }
}
