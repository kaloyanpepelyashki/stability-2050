using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul
{   
    
    /// <summary>
    /// The CommandWords class defines all valid commands the player can use
    /// and provides a method to check if a given command word is recognized.
    /// </summary>
    public class CommandWords
    {
        public List<string> ValidCommands { get; } =
            ["north", "east", "south", "west", "back", "quit", "help", "leave", "quiz"];
        
        /// <summary>
        ///  Checks if the provided string is a valid command.
        /// Returns true if it exists in the ValidCommands list, otherwise false.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>boolean</returns>
        public bool IsValidCommand(string command)
        {
            return ValidCommands.Contains(command);
        }
    }

}
